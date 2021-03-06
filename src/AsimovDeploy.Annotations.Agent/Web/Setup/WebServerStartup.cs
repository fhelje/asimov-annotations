﻿/*******************************************************************************
* Copyright (C) 2015 eBay Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*   http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
******************************************************************************/

using AsimovDeploy.Annotations.Agent.Framework;
using log4net;
using Nancy.Hosting.Self;
using StructureMap;

namespace AsimovDeploy.Annotations.Agent.Web.Setup
{
    public class WebServerStartup : IStartable
    {
        public static ILog _log = LogManager.GetLogger(typeof (WebServerStartup));
        private readonly IAnnotationsConfig _config;
        private readonly IContainer _container;
        private NancyHost _nancyHost;

        public WebServerStartup(IAnnotationsConfig config, IContainer container)
        {
            _config = config;
            _container = container;
        }

        public void Start()
        {
            _nancyHost = new NancyHost(new CustomNancyBootstrapper(_container), _config.WebControlUrl);
            _nancyHost.Start();

            _log.DebugFormat("Web server started on {0}", _config.WebControlUrl);
        }

        public void Stop()
        {
            _nancyHost.Stop();
        }

    }
}