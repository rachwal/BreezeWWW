// // Breeze Project
// // Created by Bartosz Rachwal. 
// // Copyright (c) 2016 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

namespace Breeze.SystemBuilder.Model
{
    public class SystemBuilderComponent : ISystemBuilderComponent
    {
        public bool InBuilder { get; set; }
        public string BreezeId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}