CREATE PROCEDURE [dbo].[GetTasksByUserIdBin]
    @UserId VARCHAR(MAX),
    @TaskTierId INT,
    @OrderBy INT,
    @PageSize INT,
    @PageNumber INT
AS
BEGIN

    SELECT
        [Id],
        [OriginalTaskTierId],
        [UserAppId],
        [TaskName],
        [TaskDescription],
        [IsDeleted],
        [IsCompleted],
        [CreatedUserId],
        [CreatedDate],
        [ExpectedDateTime],
        [FinishDate],
        [LastModificationDate]
    FROM
        [dbo].[TaskByUser]
    WHERE
        [CreatedUserId] = @UserId
        AND [IsDeleted] = 1
        AND (
            @TaskTierId = 0
            OR [OriginalTaskTierId] = @TaskTierId
        )
    ORDER BY
        CASE WHEN @OrderBy = 1 THEN [CreatedDate] END ASC,
        CASE WHEN @OrderBy = 2 THEN [CreatedDate] END DESC,
        CASE WHEN @OrderBy = 3 THEN [FinishDate] END ASC,
        CASE WHEN @OrderBy = 4 THEN [FinishDate] END DESC,
        [CreatedDate] ASC
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END;
GO