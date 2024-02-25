using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SRMWebApiApp.Models;

namespace SRMWebApiApp.Services {
    public interface IFileReadService {
        Task<CallSchedule> UploadData(FileStream file);
    }
}