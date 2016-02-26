// // Breeze Project
// // Created by Bartosz Rachwal. 
// // Copyright (c) 2016 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using Breeze.SystemBuilder.Model;

namespace Breeze.SystemBuilder.Adapter
{
    public interface ISystemBuilderAdapter
    {
        ISystemBuilderLayout Retrive(string id);
        void Create(string id, string content);
        void Update(string id, string content);
    }
}