using Casbin.AspNetCore.Authorization;
using Casbin.AspNetCore.Authorization.Transformers;
using Casbin.Model;
using Newtonsoft.Json;

namespace auth_casbin.Common;

/// <summary>
/// Custom Request Transformer to enable logging of casbin values for testing
/// </summary>
public class CustomRequestTransformer(ILogger<CustomRequestTransformer> logger) : BasicRequestTransformer
{
    public override ValueTask<StringRequestValues> TransformAsync(
        ICasbinAuthorizationContext<StringRequestValues> context,
        ICasbinAuthorizationData<StringRequestValues> data)
    {
        var values = data.Values;
        values.TrySetValue(0, SubTransform(context, data));
        values.TrySetValue(1, context.HttpContext.Request.Path.Value);
        values.TrySetValue(2, context.HttpContext.Request.Method);
        logger.LogInformation("Casbin authorizing for: {RequestValues}", JsonConvert.SerializeObject(values));
        return new ValueTask<StringRequestValues>(values);
    }
}
