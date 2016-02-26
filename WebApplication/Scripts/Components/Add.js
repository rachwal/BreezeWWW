$(function () {
    var inPorts = $("#add-component-in-ports-list").select2({
        tags: "true",
        placeholder: "Enter a Name",
        allowClear: true,
        minimumResultsForSearch: Infinity
    });

    var outPorts = $("#add-component-out-ports-list").select2({
        tags: "true",
        placeholder: "Enter a Name",
        allowClear: true,
        minimumResultsForSearch: Infinity
    });

    $("#add-component-save").click(function (e) {
        e.preventDefault();
        var inPortsModel = $("#add-component-in-ports-model");
        for (var i = 0; i < inPorts[0].length; i++) {
            inPortsModel.append("<input id=\"InPorts_" + i + "__Name\" name=\"InPorts[" + i + "].Name\" value=\"" + inPorts[0][i].value + "In\" type=\"hidden\">");
        }
        var outPortsModel = $("#add-component-out-ports-model");
        for (var j = 0; j < outPorts[0].length; j++) {
            outPortsModel.append("<input id=\"OutPorts_" + j + "__Name\" name=\"OutPorts[" + j + "].Name\" value=\"" + outPorts[0][j].value + "In\" type=\"hidden\">");
        }
        $("form").submit();
    });
});
