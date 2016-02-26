// // Breeze Project
// // Created by Bartosz Rachwal. 
// // Copyright (c) 2016 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

namespace Breeze.Components.Model
{
    public class Port : IPort
    {
        public string BreezeId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}