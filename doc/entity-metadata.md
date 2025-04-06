activitytypemask,int,Whether a custom activity should appear in the activity menus in the Web application.
autocreateaccessteams,boolean,Indicates whether the entity is enabled for auto created access teams.
autoroutetoownerqueue,boolean,Indicates whether to automatically move records to the owner's default queue when a record of this type is created or assigned
canbeincustomentityassociation,boolean
canbeinmanytomany,boolean,Whether the entity can be in a Many-to-Many entity relationship.
canbeprimaryentityinrelationship,boolean,Whether the entity can be the referenced entity in a One-to-Many entity relationship.
canberelatedentityinrelationship,boolean,Whether the entity can be the referencing entity in a One-to-Many entity relationship.
canchangehierarchicalrelationship,boolean,Whether the hierarchical state of entity relationships included in your managed solutions can be changed.
canchangetrackingbeenabled,boolean,For internal use only.
cancreateattributes,boolean,Whether additional attributes can be added to the entity.
cancreatecharts,boolean,Whether new charts can be created for the entity.
cancreateforms,boolean,Whether new forms can be created for the entity.
cancreateviews,boolean,Whether new views can be created for the entity.
canenablesynctoexternalsearchindex,boolean,For internal use only.
canmodifyadditionalsettings,boolean,Whether any other entity properties not represented by a managed property can be changed.
cantriggerworkflow,boolean,Whether the entity can trigger a workflow process.
changetrackingenabled,boolean,Whether change tracking is enabled for an entity.
clustermode,EntityClusterMode,Gets or sets the data that determines the behavior of the entity records when organization is part of a cluster.
collectionschemaname,string,The collection schema name of the entity.
createdon,datetime,When the table was created.
dataproviderid,Guid
datasourceid,Guid
dayssincerecordlastmodified,int
description,Label,The label containing the description for the entity.
displaycollectionname,Label,A label containing the plural display name for the entity.
displayname,Label,A label containing the display name for the entity.
enforcestatetransitions,boolean,Whether the entity will enforce custom state transitions.
entitycolor,string,The hexadecimal code to represent the color to be used for this entity in the application.
entityhelpurl,string,The URL of the resource to display help content for this entity
entityhelpurlenabled,boolean,Whether the entity supports custom help content.
entitysetname,string,The name of the Web API entity set for this entity.
externalcollectionname,string
externalname,string,The external name of an object, typically based on it's name in it's original source.
hasactivities,boolean,Whether activities are associated with this entity.
haschanged,boolean,Indicates whether the item of metadata has changed.
hasemailaddresses,boolean,Whether or not an email address is associated to this entity.
hasfeedback,boolean,Whether the entity will have a special relationship to the Feedback entity.
hasnotes,boolean,Whether notes are associated with this entity.
iconlargename,string,The name of the image web resource for the large icon for the entity.
iconmediumname,string,The name of the image web resource for the medium icon for the entity.
iconsmallname,string,The name of the image web resource for the small icon for the entity.
iconvectorname,string
introducedversion,string,A string identifying the solution version that the solution component was added in.
isactivity,boolean,Whether the entity is an activity.
isactivityparty,boolean,Whether the email messages can be sent to an email address stored in a record of this type.
isairupdated,boolean,Whether the entity uses the updated user interface.
isarchivalenabled,boolean,Whether or not the entity is enabled for archival of rows.
isauditenabled,boolean,Whether auditing has been enabled for the entity.
isavailableoffline,boolean,Whether the entity is available offline.
isbpfentity,boolean
isbusinessprocessenabled,boolean,Whether the entity is enabled for business process flows.
ischildentity,boolean,Whether the entity is a child entity.
isconnectionsenabled,boolean,Whether connections are enabled for this entity.
iscustomentity,boolean,Whether the entity is a custom entity.
iscustomizable,boolean,Whether the entity is customizable.
isdocumentmanagementenabled,boolean,Whether document management is enabled.
isdocumentrecommendationsenabled,boolean,Whether the table is enabled for document recommendations.
isduplicatedetectionenabled,boolean,Whether duplicate detection is enabled.
isenabledforcharts,boolean,Whether charts are enabled.
isenabledforexternalchannels,boolean,Whether this entity is enabled for external channels
isenabledfortrace,boolean,For internal use only.
isimportable,boolean,Whether the entity can be imported using the Import Wizard.
isinteractioncentricenabled,boolean,Whether the entity is enabled for interactive experience.
isintersect,boolean,Whether the entity is an intersection table for two other entities.
isknowledgemanagementenabled,boolean,Whether Parature knowledge management integration is enabled for the entity.
islogicalentity,boolean
ismailmergeenabled,boolean,Whether mail merge is enabled for this entity.
ismanaged,boolean,Whether the entity is part of a managed solution.
ismappable,boolean,Whether entity mapping is available for the entity.
ismsteamsintegrationenabled,boolean,Whether or not the entity is enabled for Microsoft Teams integration.
isofflineinmobileclient,boolean,Whether this entity is enabled for offline data with Dynamics 365 for tablets and Dynamics 365 for phones.
isonenoteintegrationenabled,boolean,Whether OneNote integration is enabled for the entity.
isoptimisticconcurrencyenabled,boolean,Whether optimistic concurrency is enabled for the entity
isprivate,boolean,For internal use only.
isquickcreateenabled,boolean,Whether the entity is enabled for quick create forms.
isreadingpaneenabled,boolean,For internal use only.
isreadonlyinmobileclient,boolean,Whether Microsoft Dynamics 365 for tablets users can update data for this entity.
isrenameable,boolean,Whether the entity DisplayName and DisplayCollectionName can be changed by editing the entity in the application.
isretentionenabled,boolean,Whether the table is enabled for retention.
isretrieveauditenabled,boolean
isretrievemultipleauditenabled,boolean
isslaenabled,boolean,The value indicating if the entity is enabled for service level agreements (SLAs).
issolutionaware,boolean,For internal use only.
isstatemodelaware,boolean,Whether the entity supports setting custom state transitions.
isvalidforadvancedfind,boolean,Whether the entity is will be shown in Advanced Find.
isvalidforqueue,boolean,Whether the entity is enabled for queues.
isvisibleinmobile,boolean,Whether Microsoft Dynamics 365 for phones users can see data for this entity.
isvisibleinmobileclient,boolean,Whether Microsoft Dynamics 365 for tablets users can see data for this entity.
logicalcollectionname,string,The logical collection name.
logicalname,string,The logical name for the entity.
metadataid,Guid,A unique identifier for the metadata item.
mobileofflinefilters,string
modifiedon,DateTime,When the table definition was last modified.
objecttypecode,int,The entity type code.
ownerid,Guid
owneridtype,int
ownershiptype,OwnershipTypes,The ownership type for the entity.
owningbusinessunit,Guid,The owning business unit for the entity.
primaryidattribute,string,The name of the attribute that is the primary id for the entity.
primaryimageattribute,string,The name of the primary image attribute for an entity.
primarykey,Collection( string )
primarynameattribute,string,The name of the primary attribute for an entity.
privileges,Collection(SecurityPrivilegeMetadata),The privilege metadata for the entity.
recurrencebaseentitylogicalname,string,The name of the entity that is recurring.
reportviewname,string,The name of the report view for the entity.
schemaname,string,The schema name for the entity.
settingof,string
settings,Collection(EntitySetting),For internal use only.
synctoexternalsearchindex,boolean,Whether this entity is searchable in relevance search.
tabletype,string,Whether the table is standard or elastic.
usesbusinessdatalabeltable,boolean,For internal use only.