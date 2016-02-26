// // Breeze Project
// // Created by Bartosz Rachwal. 
// // Copyright (c) 2016 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System;
using System.Collections.Generic;

namespace Breeze.Components.Model
{
    public class CoreComponent : ICoreComponent
    {
        public CoreComponent()
        {
            BreezeId = Guid.NewGuid().ToString();
            InPorts = new List<Port>();
            OutPorts = new List<Port>();
        }

        public string BreezeId { get; set; }
        public string Name { get; set; }
        public List<Port> InPorts { get; set; }
        public List<Port> OutPorts { get; set; }
    }
}