﻿using Hy.Modeller.Interfaces;
using Hy.Modeller.Models;
using Hy.Modeller.Outputs;
using System;
using System.Text;

namespace ClassLibrary
{
    internal static class ClassGenerator 
    {
        internal static IFile Create(Module module, Model model, ISettings settings)
        {
            if (module == null)
                throw new ArgumentNullException(nameof(module));
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            var sb = new StringBuilder();
            sb.al("using System;");
            sb.al("using System.Collections.Generic;");
            sb.b();
            sb.al($"namespace {module.Namespace}");
            sb.al("{");
            sb.i(1).al($"public class {model.Name}Response");
            sb.i(1).al("{");

            foreach (var item in model.Key.Fields)
            {
                sb.i(2).a($"public {item.DataType} {item.Name} {{ get; set; }}");
                sb.b();
            }

            foreach (var item in model.Fields)
            {
                sb.i(2).a($"public {item.DataType} {item.Name} {{ get; set; }}");
                sb.b();
            }


            sb.i(1).al("}");
            sb.al("}");

            var file = new File { Content = sb.ToString(), CanOverwrite = settings.SupportRegen };
            var filename = model.Name.ToString();
            if (settings.SupportRegen)
            {
                filename += ".generated";
            }
            filename += ".cs";
            file.Name = filename;
            return file;
        }
    }
}
