using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TextProcessors.Api.Models;

namespace TextProcessors.Api.Filter
{
    public class StoryModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(StoryModel))
            {
                return new BinderTypeModelBinder(typeof(StoryModelBinder));
            }

            return null;
        }
    }

}
