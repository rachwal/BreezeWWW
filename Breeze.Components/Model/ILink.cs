// // Breeze Project
// // Created by Bartosz Rachwal. 
// // Copyright (c) 2016 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

namespace Breeze.Components.Model
{
    public interface ILink
    {
        string BreezeId { get; set; }
        Connection Source { get; set; }
        Connection Target { get; set; }
    }
}