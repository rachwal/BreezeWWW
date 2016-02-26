$(function () {
    var configuration = "https://" + window.location.host + "/Breeze";

    var graph = new joint.dia.Graph;
    var paper = new joint.dia.Paper({
        el: $("#paper-mark-available"),
        width: 2000,
        height: 2000,
        gridSize: 1,
        model: graph,
        defaultLink: new joint.dia.Link({
            attrs: { '.marker-target': { d: "M 10 0 L 0 5 L 10 10 z" } }
        }),
        validateConnection: function (cellViewS, magnetS, cellViewT, magnetT, end, linkView) {
            if (magnetS && magnetS.getAttribute("type") === "input") return false;
            if (cellViewS === cellViewT) return false;
            return magnetT && magnetT.getAttribute("type") === "input";
        },
        perpendicularLinks: true,
        snapLinks: true,
        multiLinks: false,
        restrictTranslate: true,
        markAvailable: true,
        embeddingMode: true,
        linkPinning: false
    });

    $(".scroll-pane").jScrollPane({
        showArrows: true,
        maintainPosition: true,
        stickToBottom: false,
        stickToRight: false,
        clickOnTrack: true,
        autoReinitialise: true,
        autoReinitialiseDelay: 0
    });

    var area = $("#system-builder-operation-area");
    var newHeight = $(window).height() - area.offset().top;
    if (newHeight < 600) {
        newHeight = 600;
    }
    area.height(newHeight);

    var selectedItem = "none";

    paper.on("cell:pointerclick", function (evt, x, y) {
        var selectedItemName = $("#selected-item-name");
        selectedItemName.text(evt.model.attributes.breezeName);
        selectedItemName.css({ "background-color": evt.model.attributes.breezeColor, "border-color": evt.model.attributes.breezeColor, "color": "#fff" });

        if (selectedItem !== "none") {
            selectedItem.attr("rect", { fill: selectedItem.attributes.breezeColor, stroke: "#000000", 'stroke-width': 1 });
        }

        var cells = graph.getCells();
        for (var i = 0; i < cells.length; i++) {
            if (cells[i].attributes.id === evt.model.attributes.id) {
                selectedItem = cells[i];
                break;
            }
        }

        selectedItem.attr("rect", { fill: selectedItem.attributes.breezeColor, stroke: "#00c0ef", 'stroke-width': 6 });
    });

    paper.on("blank:pointerclick", function (evt, x, y) {
        $("#selected-item-name").text("");

        if (selectedItem !== "none") {
            selectedItem.attr("rect", { fill: selectedItem.attributes.breezeColor, stroke: "#000000", 'stroke-width': 1 });
        }

        selectedItem = "none";
    });

    var loadLayout = function () {
        var userId = $("#user-id").val();
        $.ajax({
            type: "GET",
            url: configuration + "/api/SystemBuilder/" + userId,
            success: function (data) {
                paper.model.fromJSON(JSON.parse(data));
            },
            failure: function (errMsg) {
                alert(errMsg);
            }
        });
    }

    var saveLayout = function () {
        var userId = $("#user-id").val();
        var serializedData = JSON.stringify(paper.model.toJSON());
        $.ajax({
            type: "POST",
            url: configuration + "/api/SystemBuilder/" + userId,
            data: JSON.stringify(serializedData),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) { },
            failure: function (errMsg) {
                alert(errMsg);
            }
        });
    }

    $("#color-chooser > li > a").click(function (e) {
        e.preventDefault();

        var currColor = $(this).css("color");

        if (selectedItem !== "none") {
            selectedItem.attributes.breezeColor = currColor;
            selectedItem.attr("rect", { fill: selectedItem.attributes.breezeColor });
        }
        $("div[data-breeze-id='" + selectedItem.attributes.breezeId + "']").css({ "background-color": currColor, "border-color": currColor, "color": "#fff" });
        $("#selected-item-name").css({ "background-color": currColor, "border-color": currColor, "color": "#fff" });
    });

    $("#paper-mark-available").droppable({
        drop: function (event, ui) {
            var element = ui.draggable[0];
            var dataset = element.dataset;
            var cells = graph.getCells();
            for (var i = 0; i < cells.length; i++) {
                if (cells[i].attributes.breezeId === dataset.breezeId) {
                    return;
                }
            }
            var parentOffset = $(this).parent().offset();
            var relX = event.pageX - parentOffset.left - 45;
            var relY = event.pageY - parentOffset.top - 45;
            var userId = $("#user-id").val();
            $.ajax({
                type: "GET",
                url: configuration + "/api/Components/" + userId,
                success: function (data) {
                    var components = JSON.parse(data);
                    var component = components[0];
                    var inPorts = [];
                    for (var i = 0; i < component.InPorts.length; i++) {
                        inPorts.push(component.InPorts[i].Name);
                    }
                    var outPorts = [];
                    for (var j = 0; j < component.OutPorts.length; j++) {
                        outPorts.push(component.OutPorts[j].Name);
                    }
                    new joint.shapes.devs.Model({
                        position: { x: relX, y: relY },
                        size: { width: 90, height: 90 },
                        inPorts: inPorts,
                        outPorts: outPorts,
                        attrs: {
                            '.label': { text: dataset.breezeName, 'ref-x': .4, 'ref-y': .2 },
                            rect: { fill: dataset.breezeColor },
                            '.inPorts circle': { fill: "#16A085", magnet: "passive", type: "input" },
                            '.outPorts circle': { fill: "#E74C3C", type: "output" }
                        },
                        breezeId: dataset.breezeId,
                        breezeName: dataset.breezeName,
                        breezeColor: dataset.breezeColor
                    }).addTo(graph);
                },
                failure: function (errMsg) {
                    alert(errMsg);
                }
            });

            if ($("#drop-remove").is(":checked")) {
                $(element).remove();
            }
        }
    });

    function initialize(e) {
        e.each(function () {
            var current = $(this);
            current.draggable({
                zIndex: 1070,
                revert: true,
                revertDuration: 0
            });
            var color = $(this).attr("data-breeze-color");
            current.css({ "background-color": color, "border-color": color, "color": "#fff" });
        });
    }

    initialize($("#external-events div.external-event"));

    function touchHandler(event) {
        var touches = event.changedTouches,
            first = touches[0],
            type = "";
        switch (event.type) {
            case "touchstart": type = "mousedown"; break;
            case "touchmove": type = "mousemove"; break;
            case "touchend": type = "click"; break;
            default: return;
        }

        var simulatedEvent = document.createEvent("MouseEvent");
        simulatedEvent.initMouseEvent(type, true, true, window, 1,
                                      first.screenX, first.screenY,
                                      first.clientX, first.clientY, false,
                                      false, false, false, 0, null);

        first.target.dispatchEvent(simulatedEvent);
        event.preventDefault();
    }

    document.addEventListener("touchend", touchHandler, true);

    $("#system-builder-save").click(function () {
        if (selectedItem !== "none") {
            selectedItem.attr("rect", { fill: selectedItem.attributes.breezeColor, stroke: "#000000", 'stroke-width': 1 });
        }
        saveLayout();
        if (selectedItem !== "none") {
            selectedItem.attr("rect", { fill: selectedItem.attributes.breezeColor, stroke: "#00c0ef", 'stroke-width': 6 });
        }
    });

    $("#system-builder-restart").click(function () {
        loadLayout();
    });

    $("#system-builder-remove-selected").click(function () {
        if (selectedItem === "none") {
            return;
        }

        var prepend = true;
        var components = $("#external-events div.external-event");
        for (var j = 0; j < components.length; j++) {
            var breezeId = $(components[j]).data("breeze-id").toString();
            if (breezeId === selectedItem.attributes.breezeId) {
                prepend = false;
            }
        }

        if (prepend === true) {
            var event = $("<div/>");
            event.attr("data-breeze-name", selectedItem.attributes.breezeName);
            event.attr("data-breeze-id", selectedItem.attributes.breezeId);
            event.attr("data-breeze-color", selectedItem.attributes.breezeColor);
            event.css({ "background-color": selectedItem.attributes.breezeColor, "border-color": selectedItem.attributes.breezeColor, "color": "#fff" }).addClass("external-event");
            var content = $("#selected-item-name").text();
            event.text(content);
            $("#external-events").prepend(event);
            initialize(event);
        }

        selectedItem.remove();
        $("#selected-item-name").text("");
        selectedItem = "none";
    });

    loadLayout();
});