﻿using QVC.TASK.Common;
using QVC.TASK.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.BL
{
    public interface IProjectBL : IBaseBL<Project>
    {
        public List<Project> GetAllProject(GetAllInput input);
    }
}
