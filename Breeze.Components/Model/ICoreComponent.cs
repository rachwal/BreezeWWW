// // Breeze Project
// // Created by Bartosz Rachwal. 
// // Copyright (c) 2016 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Collections.Generic;

namespace Breeze.Components.Model
{
    public interface ICoreComponent
    {
        string BreezeId { get; set; }
        string Name { get; set; }
        List<Port> InPorts { get; set; }
        List<Port> OutPorts { get; set; }
    }
}