// // Breeze Project
// // Created by Bartosz Rachwal. 
// // Copyright (c) 2016 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

namespace Breeze.Components.Model
{
    public interface IPort
    {
        string BreezeId { get; set; }
        string Name { get; set; }
        string Value { get; set; }
    }
}