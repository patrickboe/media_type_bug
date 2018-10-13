This bug report consists of a web api project with a default Startup
configuration (pickypicky) and a clone of the same project with a
tweaked implementation of the UnsupportedContentTypeFilter
(pickybuthelpful). The test project runs the same pair of tests on both
sites. The second test in the pair fails on the default implementation
because the ModelStateInvalidFilter catches the request before
UnsupportedContentTypeFilter has a chance to handle it. The
pickybuthelpful project demonstrates a workaround for the bug, as
demonstrated by both tests passing. The real fix would probably be to
provide a better priority in the Mvc.Core UnsupportedContentTypeFilter.
