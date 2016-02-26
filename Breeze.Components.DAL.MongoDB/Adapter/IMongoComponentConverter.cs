// // Breeze Project
// // Created by Bartosz Rachwal. 
// // Copyright (c) 2016 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Collections.Generic;
using Breeze.Components.DAL.MongoDB.Model;
using Breeze.Components.Model;

namespace Breeze.Components.DAL.MongoDB.Adapter
{
    public interface IMongoComponentConverter
    {
        ICoreComponent Convert(MongoCoreComponent mongoCoreComponent);
        IEnumerable<ICoreComponent> Convert(IEnumerable<MongoCoreComponent> mongoCoreComponents);
        MongoCoreComponent Convert(ICoreComponent coreComponent);
        IEnumerable<MongoCoreComponent> Convert(IEnumerable<ICoreComponent> coreComponent);
    }
}