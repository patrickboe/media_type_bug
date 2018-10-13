using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Filters;

namespace pickybuthelpful
{
  /*
   * A version of the UnsupportedContentTypeFilter that has a higher precedence than the
   * ModelStateInvalidFilter, so that requests with an unsupported content type will get
   * an HTTP 415 response (from this filter) instead of an HTTP 400 (from
   * ModelStateInvalidFilter)
   */
  public class HigherPrecedenceUnsupportedContentTypeFilter : UnsupportedContentTypeFilter, IOrderedFilter {
    public int Order => -2001;
  }
}
