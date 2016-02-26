// // Breeze Project
// // Created by Bartosz Rachwal. 
// // Copyright (c) 2016 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Collections.Generic;
using Breeze.SystemBuilder.Model;

namespace Breeze.SystemBuilder.Adapter
{
    public interface ISystemBuilderComponentsAdapter
    {
        IEnumerable<ISystemBuilderComponent> Get(string userId);
        void Update(string userId, ISystemBuilderComponent component);
        void Update(string userId, IEnumerable<ISystemBuilderComponent> components);
    }
}