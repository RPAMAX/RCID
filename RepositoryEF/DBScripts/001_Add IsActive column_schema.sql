
--Bird

alter table bird.Survey
add SurveyActive bit not null default 1

alter table bird.SurveyDetail
add SurveyDetailActive bit not null default 1

-- Fish
alter table fish.Survey
add SurveyActive bit not null default 1

alter table fish.SurveyDetail
add SurveyDetailActive bit not null default 1

alter table fish.SurveyLocation
add SurveyLocationActive bit not null default 1

--- Phyto
alter table phyto.Survey
add SurveyActive bit not null default 1

alter table phyto.SurveyDetail
add SurveyDetailActive bit not null default 1