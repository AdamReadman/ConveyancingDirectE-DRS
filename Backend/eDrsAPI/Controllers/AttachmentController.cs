﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eDrsManagers.Interfaces;

namespace eDrsAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly ILogsManager _logsManager;
        private readonly IAttachmentManager _attachment;

        public AttachmentController(IAttachmentManager attachment, ILogsManager logsManager)
        {
            _attachment = attachment;
            _logsManager = logsManager;
        }

        /// <summary>
        /// Get Poll Attachment 
        /// </summary>
        /// <returns>byte[]</returns>
        [HttpGet]
        public IActionResult GetApplicationPollAttached(long requestId)
        {
            try
            {
                return File(
                    _attachment.GetApplicationPollAttached(requestId),
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"

                );

            }
            catch (Exception ex)
            {
                return BadRequest(_logsManager.LogErrors(ex));
            }
        }

    }
}
