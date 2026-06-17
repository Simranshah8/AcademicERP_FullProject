Some notes of mine on programmatically managing SQL Agent jobs and schedules
----------------------------------------------------------------------------
AV, 10/05/2012


-- Listing jobs by category

SELECT * FROM sysjobs_view
WHERE category_id=...


-- Alternative way

SELECT * FROM sysjobs_view AS sv
INNER JOIN syscategories AS sc ON sv.category_id=sc.category_id
WHERE sc.name='...'


-- Listing of schedules associated to a job

sp_help_jobschedule @job_id=N'...'


--- Adding a new schedule

DECLARE @schedule_id int
EXEC msdb.dbo.sp_add_schedule @schedule_name=N'MyName', 
		@enabled=1, 
		@freq_type=8, 
		@freq_interval=19, 
		@freq_subday_type=8, 
		@freq_subday_interval=2, 
		@freq_relative_interval=0, 
		@freq_recurrence_factor=1, 
		@active_start_date=20120414, 
		@active_end_date=20120420, 
		@active_start_time=0, 
		@active_end_time=235959, @schedule_id = @schedule_id OUTPUT
SELECT @schedule_id


-- Associating a schedule to a job

EXEC msdb.dbo.sp_attach_schedule @job_id=N'...',@schedule_id=...


-- Modifying a schedule

EXEC msdb.dbo.sp_update_schedule @schedule_id=..., 
		@active_end_date=99991231


-- Detaching a schedule from a job (eventually eliminating the schedule if no more referenced)

EXEC msdb.dbo.sp_detach_schedule @job_id=N'...', @schedule_id=..., @delete_unused_schedule=1



--- Listing existing schedules

create table #tmp_sp_help_schedule
(schedule_id int null, schedule_uid uniqueidentifier null, schedule_name nvarchar(128) null, enabled int null, freq_type 
int null, freq_interval int null, freq_subday_type int null, freq_subday_interval int null, freq_relative_interval int 
null, freq_recurrence_factor int null, active_start_date int null, active_end_date int null, active_start_time int null, 
active_end_time int null, date_created datetime null, schedule_description nvarchar(4000) null, job_count int null)

insert into #tmp_sp_help_schedule exec msdb.dbo.sp_help_schedule

SELECT
tshs.schedule_name AS [Name],
tshs.schedule_id AS [ID],
null AS [ActiveEndDate],
0 AS [ActiveEndTimeOfDay],
null AS [ActiveStartDate],
0 AS [ActiveStartTimeOfDay],
tshs.date_created AS [DateCreated],
tshs.freq_interval AS [FrequencyInterval],
tshs.freq_recurrence_factor AS [FrequencyRecurrenceFactor],
tshs.freq_relative_interval AS [FrequencyRelativeIntervals],
tshs.freq_subday_interval AS [FrequencySubDayInterval],
tshs.freq_subday_type AS [FrequencySubDayTypes],
tshs.freq_type AS [FrequencyTypes],
CAST(tshs.enabled AS bit) AS [IsEnabled],
tshs.job_count AS [JobCount],
tshs.schedule_uid AS [ScheduleUid],
tshs.active_start_date AS [ActiveStartDateInt],
tshs.active_end_date AS [ActiveEndDateInt],
tshs.active_start_time AS [ActiveStartTimeOfDayInt],
tshs.active_end_time AS [ActiveEndTimeOfDayInt]
FROM #tmp_sp_help_schedule AS tshs
ORDER BY [Name] ASC

drop table #tmp_sp_help_schedule


-- Deleting a schedule

EXEC msdb.dbo.sp_delete_schedule @schedule_id=36


-- Listing all jobs
SELECT sv.name AS [Name],
sv.category_id AS [CategoryID],
sv.job_id AS [JobID]
FROM msdb.dbo.sysjobs_view AS sv
ORDER BY [Name] ASC


-- Listing job categories

exec msdb.dbo.sp_help_category

