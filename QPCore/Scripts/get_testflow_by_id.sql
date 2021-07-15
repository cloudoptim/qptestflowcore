-- FUNCTION: public.gettestflow(integer)
-- DROP FUNCTION public.gettestflow(integer);
CREATE OR REPLACE FUNCTION public.gettestflow(ptestflowid integer) RETURNS json LANGUAGE 'plpgsql' COST 100 VOLATILE PARALLEL UNSAFE AS $BODY$
DECLARE J_StepResult json;
BEGIN
SELECT row_to_json(sp) AS geoResult INTO J_StepResult
from (
        SELECT t."TestFlowId",
            t."TestFlowName",
            t."TestFlowDescription",
            t."LockedBy",
            t."TestFlowStatus",
            t."AssignedTo",
            t."AssignedDatetTime",
            t."ClientId",
            a."application_name" as "ApplicationName",
            t."LastUpdatedUserId",
            t."LastUpdatedDateTime",
            t."SourceFeatureName",
            t."SourceFeatureId",
            t."Islocked",
            t."IsActive",
            t."AreaId",
            c."CategoryName" As "AreaName",
            public.gettestflowstep(t."TestFlowId") as "Steps",
            (
                select string_agg(tfc."CategoryName", ', ')
                from public."TestFlowCategory" as tfc
                    join public."TestFlowCategoryAssoc" as tfca on tfc."CategoryId" = tfca."CategoryId"
                where tfca."TestFlowId" = ptestFlowId
            ) AS "Categories"
        FROM public."TestFlow" t
            LEFT JOIN public."TestFlowCategory" c ON t."AreaId" = c."CategoryId"
            LEFT JOIN public."Application" a ON t."ClientId" = a."client_id"
        where t."TestFlowId" = ptestFlowId
    ) as sp;
Return J_StepResult;
ENd;
$BODY$;
ALTER FUNCTION public.gettestflow(integer) OWNER TO aravin;