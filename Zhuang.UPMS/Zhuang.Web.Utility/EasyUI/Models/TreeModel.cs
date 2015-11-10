﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zhuang.Web.Utility.EasyUI.Models
{
    public enum TreeStateType
    {
        open,
        closed,
    }

    public class TreeModel
    {
        public string id { get; set; }
        public string parentId { get; set; }
        public string text { get; set; }
        public string state { get; set; }
        public bool @checked { get; set; }
        public object attributes { get; set; }
        public List<TreeModel> children { get; set; }


        public static List<TreeModel> ToTreeModel(List<TreeModel> lsRawModel)
        {
            List<TreeModel> lsResult = new List<TreeModel>();

            lsResult = lsRawModel.FindAll(c =>
            {
                return !lsRawModel.Exists(cc => cc.id == c.parentId);
            });

            lsResult.ForEach(c=> {
                c.children = RecursiveChildren(lsRawModel, c.id);
            });

            return lsResult;
        }

        private static List<TreeModel> RecursiveChildren(List<TreeModel> lsRawModel, string parentId)
        {
            var children = lsRawModel.FindAll(cc => { return cc.parentId == parentId;});

            children.ForEach(c =>
            {
                c.children = RecursiveChildren(lsRawModel, c.id);
            });

            return children;
        }
    }
}
