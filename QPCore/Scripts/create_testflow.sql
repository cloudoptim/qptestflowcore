-- FUNCTION: public.createtestflow(integer, character varying, character varying, integer, character varying, integer, date, integer, integer, date, character varying, integer, boolean, boolean)
-- DROP FUNCTION public.createtestflow(integer, character varying, character varying, integer, character varying, integer, date, integer, integer, date, character varying, integer, boolean, boolean);
CREATE OR REPLACE FUNCTION public.createtestflow(
        "P_TestFlowId" integer,
        "P_TestFlowName" character varying,
        "P_TestFlowDescription" character varying,
        "P_LockedBy" integer,
        "P_TestFlowStatus" character varying,
        "P_AssignedTo" integer,
        "P_AssignedDatetTime" date,
        "P_ClientId" integer,
        "P_LastUpdatedUserId" integer,
        "P_LastUpdatedDateTime" date,
        "P_SourceFeatureName" character varying,
        "P_SourceFeatureId" integer,
        "P_Islocked" boolean,
        "P_IsActive" boolean,
        "P_AreaId" integer
    ) RETURNS integer LANGUAGE 'plpgsql' COST 100 VOLATILE PARALLEL UNSAFE AS $BODY$
DECLARE BEGIN "P_ClientId" = COALESCE("P_ClientId", 1);
if("P_ClientId" <= 0) then "P_ClientId" = 1;
end if;
if("P_TestFlowId" <= 0) THEN
INSERT INTO public."TestFlow"(
        "TestFlowId",
        "TestFlowName",
        "TestFlowDescription",
        "LockedBy",
        "TestFlowStatus",
        "AssignedTo",
        "AssignedDatetTime",
        "ClientId",
        "LastUpdatedUserId",
        "LastUpdatedDateTime",
        "SourceFeatureName",
        "SourceFeatureId",
        "Islocked",
        "IsActive",
        "AreaId"
    )
VALUES (
        nextval('tfcolseq'),
        "P_TestFlowName",
        "P_TestFlowDescription",
        null,
        "P_TestFlowStatus",
        "P_AssignedTo",
        "P_AssignedDatetTime",
        "P_ClientId",
        "P_LastUpdatedUserId",
        CURRENT_TIMESTAMP,
        "P_SourceFeatureName",
        "P_SourceFeatureId",
        FALSE,
        TRUE,
        "P_AreaId"
    );
return currval('tfcolseq');
else
UPDATE public."TestFlow"
SET "TestFlowName" = "P_TestFlowName",
    "TestFlowDescription" = "P_TestFlowDescription",
    "TestFlowStatus" = "P_TestFlowStatus",
    "AssignedTo" = "P_AssignedTo",
    "AssignedDatetTime" = "P_AssignedDatetTime",
    "ClientId" = "P_ClientId",
    "LastUpdatedUserId" = "P_LastUpdatedUserId",
    "LastUpdatedDateTime" = CURRENT_TIMESTAMP,
    "SourceFeatureName" = "P_SourceFeatureName",
    "SourceFeatureId" = "P_SourceFeatureId",
    "AreaId" = "P_AreaId"
WHERE "TestFlowId" = "P_TestFlowId";
return "P_TestFlowId";
end If;
ENd;
$BODY$;
ALTER FUNCTION public.createtestflow(
    integer,
    character varying,
    character varying,
    integer,
    character varying,
    integer,
    date,
    integer,
    integer,
    date,
    character varying,
    integer,
    boolean,
    boolean,
    integer
) OWNER TO aravin;