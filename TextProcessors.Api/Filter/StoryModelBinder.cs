using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TextProcessors.Api.Models;

namespace TextProcessors.Api.Filter
{
    public class StoryModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            using var reader = new StreamReader(bindingContext.HttpContext.Request.Body);
            var content = reader.ReadToEndAsync().Result;

            var parts = content.Split(new[] { "---" }, StringSplitOptions.None);
            var model = new StoryModel
            {
                Word = parts[0].Trim(),
                Story = parts.Length > 1 ? parts[1].Trim() : ""
            };

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }

}
