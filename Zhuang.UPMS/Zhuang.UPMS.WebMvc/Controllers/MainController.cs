﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zhuang.Data;
using Zhuang.Models;
using Zhuang.Web.EasyUI.Models;

namespace Zhuang.UPMS.WebMvc.Controllers
{
    [Filters.CheckLogin]
    public class MainController : Controller
    {
        DbAccessor _dba = DbAccessor.Get();

        #region View
        public ActionResult Index()
        {

            var lsSecMenu = _dba.QueryEntities<SecMenu>(@"SELECT * FROM dbo.Sec_Menu
            WHERE RecordStatus='Active'");

            List<TreeUrlReturnModel> lsTree = new List<TreeUrlReturnModel>();

            foreach (var item in lsSecMenu)
            {
                lsTree.Add(new TreeUrlReturnModel()
                {
                    id = item.MenuId,
                    parentId = item.ParentId,
                    text = item.Name,
                    state = item.IsExpand ? TreeUrlReturnModel.State.open.ToString() : TreeUrlReturnModel.State.closed.ToString(),
                    attributes = new TreeUrlReturnModel.Attributes(){ url = item.Url }
                });
            }

            ViewBag.TreeModels = TreeUrlReturnModel.ToTreeUrlReturnModel(lsTree);

            return View();
        }
        #endregion

        #region Action
        public ContentResult GetMenus()
        {
            ContentResult contentResult = new ContentResult();

            var lsSecMenu = _dba.QueryEntities<SecMenu>(@"SELECT * FROM dbo.Sec_Menu
            WHERE RecordStatus='Active'");

            List<TreeUrlReturnModel> lsTree = new List<TreeUrlReturnModel>();

            foreach (var item in lsSecMenu)
            {
                lsTree.Add(new TreeUrlReturnModel()
                {
                    id = item.MenuId,
                    parentId = item.ParentId,
                    text = item.Name,
                    state = item.IsExpand ? TreeUrlReturnModel.State.open.ToString() : TreeUrlReturnModel.State.closed.ToString(),
                    attributes = new { url = item.Url }
                });
            }

            contentResult.Content = Newtonsoft.Json.JsonConvert.SerializeObject(TreeUrlReturnModel.ToTreeUrlReturnModel( lsTree));
            return contentResult;
        }
        #endregion
    }
}