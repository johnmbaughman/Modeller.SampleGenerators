﻿using DomainClass;
using Hy.Modeller.Interfaces;
using Hy.Modeller.Models;
using System;
using System.Linq;

namespace Domain
{
    public class Generator : IGenerator
    {
        private readonly Module _module;

        public Generator(ISettings settings, Module module)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _module = module ?? throw new ArgumentNullException(nameof(module));
        }

        public ISettings Settings { get; }

        public IOutput Create()
        {
            var project = (IProject)new ProjectFile(Settings, _module).Create();
            var defaultFileGroup = project.FileGroups.First();
            defaultFileGroup.AddFile((IFile)new ProjectExceptionFile(Settings, _module).Create());

            foreach (var item in _module.Models)
                project.AddFileGroup((IFileGroup)new DomainClass.Generator(Settings, _module, item).Create());

            return project;
        }
    }
}