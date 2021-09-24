using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dii_MovieCatalogSvc.Assets
{
    public class MovieCatalogSvcLogMessageTemplates : onbe.logging.templates.IOnbeLoggingMessageTemplates
    {
        public const string REQUEST_FOR_MOVIE_movieid = "Get movie request received for movie - {MovieId}";
        public const string RESPONSE_FOR_MOVIE_movieid = "Response for get movie request received for movie - {MovieId} - {@movie}";
    }
}
