using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using CountingKs.Data;
using CountingKs.Filters;
using CountingKs.Models;
using CountingKs.Services;

namespace CountingKs.Controllers
{
    [CountingKsAuthorize]
    public class DiariesController : BaseApiController
    {
        private ICountingKsIdentityService _identityService;

        public DiariesController(ICountingKsRepository repo, 
            ICountingKsIdentityService identityService) 
            : base(repo)
        {
            _identityService = identityService;
        }

        public IEnumerable<DiaryModel> Get()
        {
            // var username = Thread.CurrentPrincipal.Identity.Name;

            var username = _identityService.CurrentUser;
            var results = TheRepository.GetDiaries(username)
                .OrderByDescending(d => d.CurrentDate)
                .Take(10)
                .ToList()
                .Select(d => TheModelFactory.Create(d));

            return results;
        }

        public HttpResponseMessage Get(DateTime diaryId)
        {
            var username = _identityService.CurrentUser;
            var results = TheRepository.GetDiary(username, diaryId);

            if (results == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, 
                TheModelFactory.Create(results));
        }
    }
}
