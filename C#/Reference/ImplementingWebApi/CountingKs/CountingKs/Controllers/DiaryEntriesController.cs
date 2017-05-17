using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CountingKs.Data;
using CountingKs.Models;
using CountingKs.Services;

namespace CountingKs.Controllers
{
    public class DiaryEntriesController : BaseApiController
    {
        private ICountingKsIdentityService _identityService;

        public DiaryEntriesController(ICountingKsRepository repo, ICountingKsIdentityService identityService)
            : base(repo)
        {
            _identityService = identityService;
        }

        public IEnumerable<DiaryEntryModel> Get(DateTime diaryId)
        {
            var username = _identityService.CurrentUser;
            var results = TheRepository.GetDiaryEntries(username, diaryId.Date)
                                       .ToList()
                                       .Select(e => TheModelFactory.Create(e));

            return results;
        }

        /// <summary>
        /// Diary entry get method
        /// </summary>
        /// <param name="diaryId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(DateTime diaryId, int id)
        {
            var result = TheRepository.GetDiaryEntry(_identityService.CurrentUser, diaryId.Date, id);

            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, TheModelFactory.Create(result));
        }

        /// <summary>
        /// Diary entry post method
        /// </summary>
        /// <param name="diaryId"></param>
        /// <param name="model"></param>
        /// <returns>Based on success or error of operation, returns an HTTP Status Code
        /// with either a message, or on success, the creation of the entry entity</returns>
        public HttpResponseMessage Post(DateTime diaryId, [FromBody]DiaryEntryModel model)
        {
            try
            {
                var entity = TheModelFactory.Parse(model);

                if (entity == null)
                {
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not read diary entry in body");
                }

                var diary = TheRepository.GetDiary(_identityService.CurrentUser, diaryId);

                if (diary == null)
                {
                    Request.CreateResponse(HttpStatusCode.NotFound);
                }

                // Make sure it's not duplicate
                if (diary.Entries.Any(e => e.Measure.Id == entity.Measure.Id))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Duplicate measure not allowed");
                }

                diary.Entries.Add(entity);

                if (TheRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, TheModelFactory.Create(entity));
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Write to DB not successfull");
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// Diary entry deletion method
        /// </summary>
        /// <param name="diaryId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(DateTime diaryId, int id)
        {
            try
            {
                if (TheRepository.GetDiaryEntries(_identityService.CurrentUser, diaryId)
                    .Any(e => e.Id == id) == false)
                {
                    Request.CreateResponse(HttpStatusCode.NotFound);
                }

                if (TheRepository.DeleteDiaryEntry(id) && TheRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest); 
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex); 
            }
        }

        /// <summary>
        /// Diary entry patch method
        /// </summary>
        /// <param name="diaryId"></param>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [HttpPatch]
        public HttpResponseMessage Patch(DateTime diaryId, int id, 
            [FromBody] DiaryEntryModel model)
        {
            try
            {
                var entity = TheRepository.GetDiaryEntry(_identityService.CurrentUser, diaryId, id);

                if (entity == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Diary entry not found!");
                }

                var parsedValue = TheModelFactory.Parse(model);
                if (parsedValue == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Parsed value is null!");
                }

                if (entity.Quantity != parsedValue.Quantity)
                {
                    entity.Quantity = parsedValue.Quantity;

                    if (TheRepository.SaveAll())
                    {
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request!");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
