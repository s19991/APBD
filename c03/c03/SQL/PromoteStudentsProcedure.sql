

create procedure promote_students @studies_name nvarchar(100), @semester int
as
    begin
        set xact_abort on
        begin tran
            declare @id_studies int
            select @id_studies = s.idstudy from studies s where s.name = @studies_name
            if @id_studies is null
            begin
                raiserror (50000, 1, 1)
                return;
            end;

            declare @previous_enrollment_id int;
            declare @new_enrollment_id int;

            select @previous_enrollment_id = e.idenrollment from enrollment e
            where e.idstudy = @id_studies and e.semester = @semester;

            select @new_enrollment_id = e.idenrollment from enrollment e
            where e.idstudy = @id_studies and e.semester = @semester+1;
            if @new_enrollment_id is null
            begin
                declare @new_id int;
                select @new_id = max(e.idenrollment)+1 from enrollment e;
                insert into enrollment values (@new_id, @semester+1, @id_studies, getdate());
                set @new_enrollment_id = @new_id;
            end
            update student set idenrollment = @new_enrollment_id where idenrollment = @previous_enrollment_id;

            commit;
    end