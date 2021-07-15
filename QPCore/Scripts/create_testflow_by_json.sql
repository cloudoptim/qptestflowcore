-- FUNCTION: public.create_test_using_json(json, integer)
-- DROP FUNCTION public.create_test_using_json(json, integer);
CREATE OR REPLACE FUNCTION public.create_test_using_json(j_testflow json, user_id integer) RETURNS json LANGUAGE 'plpgsql' COST 100 VOLATILE PARALLEL UNSAFE AS $BODY$
DECLARE
declare l_WorkflowId int;
declare l_stepId int;
declare l_colId int;
declare stepColumns json;
declare stepRows json;
Declare elementJson json;
declare step json;
BEGIN
select public.createtestflow(
        (j_testflow->>'TestFlowId')::int,
        j_testflow->>'TestFlowName',
        j_testflow->>'TestFlowDescription',
        (j_testflow->>'LockedBy')::int,
        j_testflow->>'TestFlowStatus',
        (j_testflow->>'AssignedTo')::int,
        (j_testflow->>'AssignedDatetTime')::date,
        (j_testflow->>'ClientId')::int,
        user_id,
        (j_testflow->>'LastUpdatedDateTime')::date,
        j_testflow->>'SourceFeatureName',
        (j_testflow->>'SourceFeatureId')::int,
        (j_testflow->>'Islocked')::boolean,
        (j_testflow->>'IsActive')::boolean,
        (j_testflow->>'AreaId')::int
    ) into l_WorkflowId;
Delete from public."TestFlowRow"
where "ColumnId" in (
        Select "ColumnId"
        from public."TestFlowColumn"
        where "TestFlowStepId" in (
                Select "TestFlowStepId"
                from public."TestFlowStep"
                where "TestFlowId" = l_WorkflowId
            )
    );
Delete from public."TestFlowColumn"
where "TestFlowStepId" in (
        Select "TestFlowStepId"
        from public."TestFlowStep"
        where "TestFlowId" = l_WorkflowId
    );
Delete from public."TestFlowStep"
where "TestFlowId" = l_WorkflowId;
FOR step IN
SELECT *
FROM json_array_elements((j_testflow->>'Steps')::json) LOOP
select public.createtestflowstep(
        (step->>'TestFlowStepId')::int,
        (step->>'StepGlossaryStepId')::int,
        step->>'TestFlowStepName',
        step->>'TestFlowStepDescription',
        step->>'TestFlowStepType',
        step->>'TestFlowStepDataType',
        step->>'TestFlowStepSource',
        (step->>'ClientId')::int,
        (step->>'IsActive')::boolean,
        l_WorkflowId,
        (step->>'OrderNumber')::int,
        step->>'ResourceType'
    ) into l_stepId;
FOR StepColumns IN
SELECT *
FROM json_array_elements((step->>'Columns')::json) LOOP
Select public.createtfcloumn(
        (stepColumns->>'ColumnId')::int,
        stepColumns->>'ColumnName',
        (stepColumns->>'ColumnIndex')::int,
        l_stepId
    ) into l_colId;
RAISE NOTICE 'Group Parsing Item % %',
StepColumns->>'ColumnId',
StepColumns->>'ColumnName';
FOR stepRows IN
SELECT *
FROM json_array_elements((StepColumns->>'Rows')::json) LOOP Perform public.createtfrow(
        (stepRows->>'RowId')::int,
        stepRows->>'RowValue',
        l_colId,
        (stepRows->>'RowNumber')::int
    );
END LOOP;
END LOOP;
End LOOP;
RAISE NOTICE 'Model id%',
l_WorkflowId;
return public.gettestflow(l_WorkflowId);
ENd;
$BODY$;
ALTER FUNCTION public.create_test_using_json(json, integer) OWNER TO aravin;