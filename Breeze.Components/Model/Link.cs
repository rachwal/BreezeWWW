﻿// // Breeze Project
// // Created by Bartosz Rachwal. 
// // Copyright (c) 2016 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

namespace Breeze.Components.Model
{
    public class Link : ILink
    {
        public string BreezeId { get; set; }
        public Connection Source { get; set; }
        public Connection Target { get; set; }
    }
}