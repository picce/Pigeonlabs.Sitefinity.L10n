# Pigeonlabs.Sitefinity.L10n
Helper class for sitefinity localization, ideal for api l10n

### How to use with localized reousrces
```csharp
//CurrentLangCode could be set manually 
//or from requestContext.RouteData.Values["lang"] if in a controller 
var R = new L10n(CurrentLangCode);
R.GetRes("YpurModuleResourcesClass", "ReourceEntryKey", "dafault value here");
```

### How to use inside an ApiController
```csharp
using Pigeonlabs.Sitefinity;

public class MyApiController : ApiController
{
  var res = new MyResultModel();
  
  //retrieve AcceptLanguage from request header
  string acceptLang = Request?.Headers?.AcceptLanguage?.FirstOrDefault().Value;
  
  var R = new L10n(acceptLang);
  
  DynamicModuleManager dmm = DynamicModuleManager.GetManager(string.Empty);
  Type contentType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.AcmeCompany.YourItemName");
  var list = dmm.GetDataItems(contentType).Where(p => p.Status == ContentLifecycleStatus.Live);
  foreach (var dyn in list)
  {
      var itemModel = new YourItemModel();
      itemModel.Id = dyn.Id.ToString();
      //retrieve value in the right language
      itemModel.Title = R.DynString(dyn, "Title");      
      itemModel.Abstract = R.DynString(dyn, "Abstract");
      
      res.YourItemsList.Add(itemModel);
  }
  return Ok(res);
}
```
