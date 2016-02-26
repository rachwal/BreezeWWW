// // Breeze Project
// // Created by Bartosz Rachwal. 
// // Copyright (c) 2016 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Collections.Generic;
using Breeze.Components.Model;

namespace Breeze.Components.Adapter
{
    public interface ICoreComponentsAdapter
    {
        IEnumerable<ICoreComponent> Get(string userId);
        void Update(string userId, ICoreComponent coreComponent);
    }
}