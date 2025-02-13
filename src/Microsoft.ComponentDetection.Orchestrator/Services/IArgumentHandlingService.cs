﻿using System.Threading.Tasks;
using Microsoft.ComponentDetection.Contracts.BcdeModels;
using Microsoft.ComponentDetection.Orchestrator.ArgumentSets;

namespace Microsoft.ComponentDetection.Orchestrator.Services
{
    public interface IArgumentHandlingService
    {
        bool CanHandle(IScanArguments arguments);

        Task<ScanResult> Handle(IScanArguments arguments);
    }
}
