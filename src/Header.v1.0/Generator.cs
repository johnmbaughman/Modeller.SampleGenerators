﻿using System;
using Hy.Modeller.Interfaces;
using Hy.Modeller.Outputs;

namespace Header
{
    public class Generator : IGenerator
    {
        private readonly IMetadata _metadata;

        public Generator(ISettings settings, IMetadata metadata)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
        }

        public ISettings Settings { get; }

        public IOutput Create()
        {
            var sb = new System.Text.StringBuilder();
            sb.al($"// Created using Hy.Modeller template '{_metadata.Name}' version {_metadata.Version}");
            sb.al($"// NOTE: This file cannot be overwritten when regenerated");
            return new Snippet(sb.ToString());
        }
    }
}