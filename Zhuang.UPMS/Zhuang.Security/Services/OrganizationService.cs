﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zhuang.Data;
using Zhuang.Models;

namespace Zhuang.Security.Services
{
    public class OrganizationService
    {
        DbAccessor _dba = DbAccessor.Get();

        public SecOrganization GetOrganizationById(string organizationId)
        {
            return _dba.QueryEntity<SecOrganization>("Security.SecOrganization.GetSecOrganizationById", new { OrganizationId = organizationId });
        }

        public void DeleteRecursive(string organizationId)
        {
            string strSql = "select * from Sec_Organization where ParentId=#ParentId#";
            var children = _dba.QueryEntities<SecOrganization>(strSql, new { ParentId = organizationId });
            foreach (var item in children)
            {
                DeleteRecursive(item.OrganizationId);
            }
            _dba.ExecuteNonQuery("Security.Organization.Delete", new { OrganizationId = organizationId });
        }
    }
}
