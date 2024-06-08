using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.Entity.Entities.Videos;

namespace PsychosocialSupportPlatformAPI.DataAccess.Statistics
{
    public class VideoStatisticsRepository : IVideoStatisticsRepository
    {
        private readonly PsychosocialSupportPlatformDBContext _context;
        public VideoStatisticsRepository(PsychosocialSupportPlatformDBContext context)
        {
            _context = context;
        }
        public async Task CreateVideoStatistics(VideoStatistics statistics, CancellationToken cancellationToken)
        {
            await _context.VideoStatistics.AddAsync(statistics, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteVideoStatistics(int statisticsId, CancellationToken cancellationToken)
        {
            var deleteVideoStatistics = await _context.VideoStatistics.Where(s => s.Id == statisticsId).FirstAsync(cancellationToken);

            if (deleteVideoStatistics != null)
            {
                _context.VideoStatistics.Remove(deleteVideoStatistics);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<IEnumerable<object>> GetAllVideoStatistics(CancellationToken cancellationToken)
        {
            return await _context.Videos.Select(v => new
            {
                VideoId = v.Id,
                VideoTitle = v.Title,
                VideoStatistics = v.Statistics.Select(s => new
                {
                    PatientNameId = s.PatientId,
                    PatientName = s.Patient.Name,
                    PatientSurname = s.Patient.Surname,
                    PatientProfileImageUrl = s.Patient.ProfileImageUrl,
                    VideoClicksNumber = s.ClicksNumber,
                    VideoViewingRate = s.ViewingRate
                }),

            }).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<object>> GetAllVideoStatisticsByPatientId(string patientId, CancellationToken cancellationToken)
        {
            return await _context.Videos
                .AsNoTracking()
                .Select(v => new
                {
                    VideoId = v.Id,
                    VideoTitle = v.Title,
                    VideoStatistics = v.Statistics.Where(s => s.PatientId == patientId).Select(s => new
                    {
                        PatientProfileImageUrl = s.Patient.ProfileImageUrl,
                        VideoClicksNumber = s.ClicksNumber,
                        VideoViewingRate = s.ViewingRate
                    }),
                }).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<object>> GetAllVideoStatisticsByPatientUserName(string patientUserName, CancellationToken cancellationToken)
        {
            return await _context.Patients
                .AsNoTracking()
                .Where(p => p.UserName == patientUserName)
                .Select(p => new
                {
                    PatientName = p.Name,
                    PatientSurname = p.Surname,
                    PatientProfileImageUrl = p.ProfileImageUrl,

                    Videos = _context.Videos.Select(v => new
                    {
                        VideoId = v.Id,
                        VideoTitle = v.Title,
                        VideoStatistics = p.Statistics.Where(s => s.VideoId == v.Id).Select(s => new
                        {
                            VideoClicksNumber = s.ClicksNumber,
                            VideoViewingRate = s.ViewingRate
                        }).ToList(),
                    }).ToList(),

                }).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<object>> GetAllVideoStatisticsByPatientUserName(string patientUserName, string doctorId, CancellationToken cancellationToken)
        {
            return await _context.Patients
                .AsNoTracking()
                .Include(p => p.AppointmentSchedules)
                .Where(p => p.UserName == patientUserName && p.AppointmentSchedules.Any(a => a.DoctorId == doctorId))
                .Select(p => new
                {
                    PatientName = p.Name,
                    PatientSurname = p.Surname,
                    PatientProfileImageUrl = p.ProfileImageUrl,

                    Videos = _context.Videos.Select(v => new
                    {
                        VideoId = v.Id,
                        VideoTitle = v.Title,
                        VideoStatistics = p.Statistics.Where(s => s.VideoId == v.Id).Select(s => new
                        {
                            VideoClicksNumber = s.ClicksNumber,
                            VideoViewingRate = s.ViewingRate
                        }).ToList(),
                    }).ToList(),
                }).ToListAsync(cancellationToken);
        }

        public async Task<VideoStatistics?> GetPatientVideoStatisticsByVideoId(string patientId, int videoId, CancellationToken cancellationToken)
        {
            return await _context.VideoStatistics
                .AsNoTracking()
                .Where(v => v.VideoId == videoId && v.PatientId == patientId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<VideoStatistics?> GetVideoStatisticsById(int statisticsId, CancellationToken cancellationToken)
        {
            return await _context.VideoStatistics.AsNoTracking().Where(v => v.Id == statisticsId).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task UpdateVideoStatistics(VideoStatistics statistics, CancellationToken cancellationToken)
        {
            _context.VideoStatistics.Update(statistics);
            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
