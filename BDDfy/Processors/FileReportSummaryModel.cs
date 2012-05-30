﻿// Copyright (C) 2011, Mehdi Khalili
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System.Collections.Generic;
using System.Linq;
using BDDfy.Core;

namespace BDDfy.Processors
{
    public class FileReportSummaryModel
    {
        readonly IEnumerable<Story> _stories;
        readonly IEnumerable<Scenario> _scenarios;

        public FileReportSummaryModel(IEnumerable<Story> stories)
        {
            _stories = stories;
            _scenarios = _stories.SelectMany(s => s.Scenarios).ToList();
        }

        public int Namespaces
        {
            get
            {
                return _stories.Where(b => b.MetaData == null)
                    .SelectMany(s => s.Scenarios).GroupBy(b => b.TestObject.GetType().Namespace).Count();
            }
        }

        public int Scenarios
        {
            get { return _stories.SelectMany(s => s.Scenarios).Count(); }
        }

        public int Stories
        {
            get { return _stories.Where(b => b.MetaData != null).GroupBy(b => b.MetaData.Type).Count(); }
        }

        public int Passed
        {
            get { return _scenarios.Count(b => b.Result == StepExecutionResult.Passed); }
        }

        public int Failed
        {
            get { return _scenarios.Count(b => b.Result == StepExecutionResult.Failed); }
        }

        public int Inconclusive
        {
            get { return _scenarios.Count(b => b.Result == StepExecutionResult.Inconclusive); }
        }

        public int NotImplemented
        {
            get { return _scenarios.Count(b => b.Result == StepExecutionResult.NotImplemented); }
        }
    }
}