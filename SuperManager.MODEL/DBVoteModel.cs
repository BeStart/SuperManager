﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.MODEL
{
    public class DBVoteModel
    {
        public int IdentityID { get; set; }
        /// <summary>
        /// 投票类别
        /// </summary>
        public int VoteType { get; set; }
        /// <summary>
        /// 投票标题
        /// </summary>
        public string VoteTitle { get; set; }
        /// <summary>
        /// 投票简介
        /// </summary>
        public string VoteSummary { get; set; }
    }
}
