// MVVM templates for API entities
using System;
using System.Collections.ObjectModel;
using System.Linq;
using static Utilities.Extensions.TypeEx;

namespace Downwork_Notifier.ViewModels.API.ApiEntities.Auth
{
	public partial class AuthUserViewModel : BaseApiViewModel<ApiLibrary.ApiEntities.Auth.AuthUser>
	{
		#region Constructors
		public AuthUserViewModel() : base()
		{
			Location = new LocationViewModel();
			Capacity = new CapacityViewModel();
			
		}
		public AuthUserViewModel(ApiLibrary.ApiEntities.Auth.AuthUser originalEntity) : base(originalEntity)
		{
			Location = new LocationViewModel(originalEntity.Location);
			Capacity = new CapacityViewModel(originalEntity.Capacity);
			
		}
		#endregion Constructors

		#region Properties

		#region Filling entity fields with viewmodels-fields entity values
		public override ApiLibrary.ApiEntities.Auth.AuthUser Entity
		{
			get
			{
				var baseEntity = base.Entity;
				baseEntity.Location = Location.Entity;
				baseEntity.Capacity = Capacity.Entity;
				return baseEntity;
			}
		}
		#endregion Filling entity fields with viewmodels-fields entity values

		#region References
		public System.Int32 Reference
		{
			get { return Entity.Reference; }
			set { SetProperty(Entity, e => e.Reference, value); }
		}

		public System.Uri ProfileUrl
		{
			get { return Entity.ProfileUrl; }
			set { SetProperty(Entity, e => e.ProfileUrl, value); }
		}

		public LocationViewModel Location
		{
			get { return _Location; }
			set { SetProperty(ref _Location, value); }
		}
		private LocationViewModel _Location;

		public CapacityViewModel Capacity
		{
			get { return _Capacity; }
			set { SetProperty(ref _Capacity, value); }
		}
		private CapacityViewModel _Capacity;

		public System.Boolean HasAgency
		{
			get { return Entity.HasAgency; }
			set { SetProperty(Entity, e => e.HasAgency, value); }
		}

		public System.Uri CompanyUrl
		{
			get { return Entity.CompanyUrl; }
			set { SetProperty(Entity, e => e.CompanyUrl, value); }
		}

		public System.Uri Portrait32Url
		{
			get { return Entity.Portrait32Url; }
			set { SetProperty(Entity, e => e.Portrait32Url, value); }
		}

		public System.Uri Portrait50Url
		{
			get { return Entity.Portrait50Url; }
			set { SetProperty(Entity, e => e.Portrait50Url, value); }
		}

		public System.Uri Portrait100Url
		{
			get { return Entity.Portrait100Url; }
			set { SetProperty(Entity, e => e.Portrait100Url, value); }
		}

		#endregion References

		#endregion Properties
	}

	public partial class CapacityViewModel : BaseApiViewModel<ApiLibrary.ApiEntities.Auth.Capacity>
	{
		#region Constructors
		public CapacityViewModel() : base()
		{
			
		}
		public CapacityViewModel(ApiLibrary.ApiEntities.Auth.Capacity originalEntity) : base(originalEntity)
		{
			
		}
		#endregion Constructors

		#region Properties

		#region Filling entity fields with viewmodels-fields entity values
		public override ApiLibrary.ApiEntities.Auth.Capacity Entity
		{
			get
			{
				var baseEntity = base.Entity;
				return baseEntity;
			}
		}
		#endregion Filling entity fields with viewmodels-fields entity values

		#region References
		public System.Boolean IsProvider
		{
			get { return Entity.IsProvider; }
			set { SetProperty(Entity, e => e.IsProvider, value); }
		}

		public System.Boolean IsBuyer
		{
			get { return Entity.IsBuyer; }
			set { SetProperty(Entity, e => e.IsBuyer, value); }
		}

		public System.Boolean IsAffiliateManager
		{
			get { return Entity.IsAffiliateManager; }
			set { SetProperty(Entity, e => e.IsAffiliateManager, value); }
		}

		#endregion References

		#endregion Properties
	}

	public partial class LocationViewModel : BaseApiViewModel<ApiLibrary.ApiEntities.Auth.Location>
	{
		#region Constructors
		public LocationViewModel() : base()
		{
			
		}
		public LocationViewModel(ApiLibrary.ApiEntities.Auth.Location originalEntity) : base(originalEntity)
		{
			
		}
		#endregion Constructors

		#region Properties

		#region Filling entity fields with viewmodels-fields entity values
		public override ApiLibrary.ApiEntities.Auth.Location Entity
		{
			get
			{
				var baseEntity = base.Entity;
				return baseEntity;
			}
		}
		#endregion Filling entity fields with viewmodels-fields entity values

		#region References
		public System.String City
		{
			get { return Entity.City; }
			set { SetProperty(Entity, e => e.City, value); }
		}

		public System.String State
		{
			get { return Entity.State; }
			set { SetProperty(Entity, e => e.State, value); }
		}

		public System.String Country
		{
			get { return Entity.Country; }
			set { SetProperty(Entity, e => e.Country, value); }
		}

		#endregion References

		#endregion Properties
	}

}

namespace Downwork_Notifier.ViewModels.API.ApiEntities.HR
{
	public partial class UserViewModel : BaseApiViewModel<ApiLibrary.ApiEntities.HR.User>
	{
		#region Constructors
		public UserViewModel() : base()
		{
			
		}
		public UserViewModel(ApiLibrary.ApiEntities.HR.User originalEntity) : base(originalEntity)
		{
			
		}
		#endregion Constructors

		#region Properties

		#region Filling entity fields with viewmodels-fields entity values
		public override ApiLibrary.ApiEntities.HR.User Entity
		{
			get
			{
				var baseEntity = base.Entity;
				return baseEntity;
			}
		}
		#endregion Filling entity fields with viewmodels-fields entity values

		#region References
		public System.String Id
		{
			get { return Entity.Id; }
			set { SetProperty(Entity, e => e.Id, value); }
		}

		public System.Int32 Reference
		{
			get { return Entity.Reference; }
			set { SetProperty(Entity, e => e.Reference, value); }
		}

		public System.Boolean IsProvider
		{
			get { return Entity.IsProvider; }
			set { SetProperty(Entity, e => e.IsProvider, value); }
		}

		public ApiLibrary.ApiEntities.HR.UserStatus Status
		{
			get { return Entity.Status; }
			set { SetProperty(Entity, e => e.Status, value); }
		}

		public System.String FirstName
		{
			get { return Entity.FirstName; }
			set { SetProperty(Entity, e => e.FirstName, value); }
		}

		public System.String LastName
		{
			get { return Entity.LastName; }
			set { SetProperty(Entity, e => e.LastName, value); }
		}

		public System.String Email
		{
			get { return Entity.Email; }
			set { SetProperty(Entity, e => e.Email, value); }
		}

		public System.String Timezone
		{
			get { return Entity.Timezone; }
			set { SetProperty(Entity, e => e.Timezone, value); }
		}

		public System.Int32 TimezoneOffset
		{
			get { return Entity.TimezoneOffset; }
			set { SetProperty(Entity, e => e.TimezoneOffset, value); }
		}

		public System.Uri PublicUrl
		{
			get { return Entity.PublicUrl; }
			set { SetProperty(Entity, e => e.PublicUrl, value); }
		}

		public System.String ProfileKey
		{
			get { return Entity.ProfileKey; }
			set { SetProperty(Entity, e => e.ProfileKey, value); }
		}

		#endregion References

		#endregion Properties
	}

	public partial class UserRoleViewModel : BaseApiViewModel<ApiLibrary.ApiEntities.HR.UserRole>
	{
		#region Constructors
		public UserRoleViewModel() : base()
		{
			Permissions = new ObservableCollection<PermissionSetViewModel>();
			
		}
		public UserRoleViewModel(ApiLibrary.ApiEntities.HR.UserRole originalEntity) : base(originalEntity)
		{
			Permissions = new ObservableCollection<PermissionSetViewModel>(originalEntity.Permissions?.Select(e => new PermissionSetViewModel(e)) ?? new PermissionSetViewModel[0]);
			
		}
		#endregion Constructors

		#region Properties

		#region Filling entity fields with viewmodels-fields entity values
		public override ApiLibrary.ApiEntities.HR.UserRole Entity
		{
			get
			{
				var baseEntity = base.Entity;
				baseEntity.Permissions = Permissions.Select(_ => _.Entity).ToArray();
				return baseEntity;
			}
		}
		#endregion Filling entity fields with viewmodels-fields entity values

		#region Collections
		public ObservableCollection<PermissionSetViewModel> Permissions
		{
			get { return _Permissions; }
			set { SetProperty(ref _Permissions, value); }
		}
		private ObservableCollection<PermissionSetViewModel> _Permissions = new ObservableCollection<PermissionSetViewModel>();
		#endregion Collections

		#region References
		public System.String ParentTeamId
		{
			get { return Entity.ParentTeamId; }
			set { SetProperty(Entity, e => e.ParentTeamId, value); }
		}

		public System.String UserFirstName
		{
			get { return Entity.UserFirstName; }
			set { SetProperty(Entity, e => e.UserFirstName, value); }
		}

		public System.Int32 CompanyReference
		{
			get { return Entity.CompanyReference; }
			set { SetProperty(Entity, e => e.CompanyReference, value); }
		}

		public System.String UserLastName
		{
			get { return Entity.UserLastName; }
			set { SetProperty(Entity, e => e.UserLastName, value); }
		}

		public System.Boolean IsTeamHidden
		{
			get { return Entity.IsTeamHidden; }
			set { SetProperty(Entity, e => e.IsTeamHidden, value); }
		}

		public System.Int32 Reference
		{
			get { return Entity.Reference; }
			set { SetProperty(Entity, e => e.Reference, value); }
		}

		public System.Int32 TeamReference
		{
			get { return Entity.TeamReference; }
			set { SetProperty(Entity, e => e.TeamReference, value); }
		}

		public System.String AffiliationStatus
		{
			get { return Entity.AffiliationStatus; }
			set { SetProperty(Entity, e => e.AffiliationStatus, value); }
		}

		public System.Int32 UserReference
		{
			get { return Entity.UserReference; }
			set { SetProperty(Entity, e => e.UserReference, value); }
		}

		public System.Boolean IsUserProvider
		{
			get { return Entity.IsUserProvider; }
			set { SetProperty(Entity, e => e.IsUserProvider, value); }
		}

		public System.String ParentTeamName
		{
			get { return Entity.ParentTeamName; }
			set { SetProperty(Entity, e => e.ParentTeamName, value); }
		}

		public System.Boolean HasTeamRoomAccess
		{
			get { return Entity.HasTeamRoomAccess; }
			set { SetProperty(Entity, e => e.HasTeamRoomAccess, value); }
		}

		public System.Int32 ParentTeamReference
		{
			get { return Entity.ParentTeamReference; }
			set { SetProperty(Entity, e => e.ParentTeamReference, value); }
		}

		public System.String TeamId
		{
			get { return Entity.TeamId; }
			set { SetProperty(Entity, e => e.TeamId, value); }
		}

		public System.String EngagementReference
		{
			get { return Entity.EngagementReference; }
			set { SetProperty(Entity, e => e.EngagementReference, value); }
		}

		public System.String TeamName
		{
			get { return Entity.TeamName; }
			set { SetProperty(Entity, e => e.TeamName, value); }
		}

		public System.String CompanyName
		{
			get { return Entity.CompanyName; }
			set { SetProperty(Entity, e => e.CompanyName, value); }
		}

		public System.String Role
		{
			get { return Entity.Role; }
			set { SetProperty(Entity, e => e.Role, value); }
		}

		public System.String UserId
		{
			get { return Entity.UserId; }
			set { SetProperty(Entity, e => e.UserId, value); }
		}

		public System.Boolean IsOwner
		{
			get { return Entity.IsOwner; }
			set { SetProperty(Entity, e => e.IsOwner, value); }
		}

		#endregion References

		#endregion Properties
	}

	public partial class PermissionSetViewModel : BaseApiViewModel<ApiLibrary.ApiEntities.HR.PermissionSet>
	{
		#region Constructors
		public PermissionSetViewModel() : base()
		{
			Permissions = new ObservableCollection<ApiLibrary.ApiEntities.HR.UserRolePermission>();
			
		}
		public PermissionSetViewModel(ApiLibrary.ApiEntities.HR.PermissionSet originalEntity) : base(originalEntity)
		{
			Permissions = new ObservableCollection<ApiLibrary.ApiEntities.HR.UserRolePermission>(originalEntity.Permissions ?? new ApiLibrary.ApiEntities.HR.UserRolePermission[0]);
			
		}
		#endregion Constructors

		#region Properties

		#region Filling entity fields with viewmodels-fields entity values
		public override ApiLibrary.ApiEntities.HR.PermissionSet Entity
		{
			get
			{
				var baseEntity = base.Entity;
				return baseEntity;
			}
		}
		#endregion Filling entity fields with viewmodels-fields entity values

		#region Collections
		public ObservableCollection<ApiLibrary.ApiEntities.HR.UserRolePermission> Permissions
		{
			get { return _Permissions; }
			set { SetProperty(ref _Permissions, value); }
		}
		private ObservableCollection<ApiLibrary.ApiEntities.HR.UserRolePermission> _Permissions = new ObservableCollection<ApiLibrary.ApiEntities.HR.UserRolePermission>();
		#endregion Collections

		#endregion Properties
	}

}

namespace Downwork_Notifier.ViewModels.API.ApiEntities.Metadata
{
	public partial class CategoryViewModel : BaseApiViewModel<ApiLibrary.ApiEntities.Metadata.Category>
	{
		#region Constructors
		public CategoryViewModel() : base()
		{
			SubCategories = new ObservableCollection<CategoryViewModel>();
			
		}
		public CategoryViewModel(ApiLibrary.ApiEntities.Metadata.Category originalEntity) : base(originalEntity)
		{
			SubCategories = new ObservableCollection<CategoryViewModel>(originalEntity.SubCategories?.Select(e => new CategoryViewModel(e)) ?? new CategoryViewModel[0]);
			
		}
		#endregion Constructors

		#region Properties

		#region Filling entity fields with viewmodels-fields entity values
		public override ApiLibrary.ApiEntities.Metadata.Category Entity
		{
			get
			{
				var baseEntity = base.Entity;
				baseEntity.SubCategories = SubCategories.Select(_ => _.Entity).ToArray();
				return baseEntity;
			}
		}
		#endregion Filling entity fields with viewmodels-fields entity values

		#region Collections
		public ObservableCollection<CategoryViewModel> SubCategories
		{
			get { return _SubCategories; }
			set { SetProperty(ref _SubCategories, value); }
		}
		private ObservableCollection<CategoryViewModel> _SubCategories = new ObservableCollection<CategoryViewModel>();
		#endregion Collections

		#region References
		public System.String Title
		{
			get { return Entity.Title; }
			set { SetProperty(Entity, e => e.Title, value); }
		}

		public System.Int64 Id
		{
			get { return Entity.Id; }
			set { SetProperty(Entity, e => e.Id, value); }
		}

		#endregion References

		#endregion Properties
	}

	public partial class RegionViewModel : BaseApiViewModel<ApiLibrary.ApiEntities.Metadata.Region>
	{
		#region Constructors
		public RegionViewModel() : base()
		{
			
		}
		public RegionViewModel(ApiLibrary.ApiEntities.Metadata.Region originalEntity) : base(originalEntity)
		{
			
		}
		#endregion Constructors

		#region Properties

		#region Filling entity fields with viewmodels-fields entity values
		public override ApiLibrary.ApiEntities.Metadata.Region Entity
		{
			get
			{
				var baseEntity = base.Entity;
				return baseEntity;
			}
		}
		#endregion Filling entity fields with viewmodels-fields entity values

		#region References
		public System.String Alias
		{
			get { return Entity.Alias; }
			set { SetProperty(Entity, e => e.Alias, value); }
		}

		public System.String Title
		{
			get { return Entity.Title; }
			set { SetProperty(Entity, e => e.Title, value); }
		}

		#endregion References

		#endregion Properties
	}

	public partial class SkillViewModel : BaseApiViewModel<ApiLibrary.ApiEntities.Metadata.Skill>
	{
		#region Constructors
		public SkillViewModel() : base()
		{
			
		}
		public SkillViewModel(ApiLibrary.ApiEntities.Metadata.Skill originalEntity) : base(originalEntity)
		{
			
		}
		#endregion Constructors

		#region Properties

		#region Filling entity fields with viewmodels-fields entity values
		public override ApiLibrary.ApiEntities.Metadata.Skill Entity
		{
			get
			{
				var baseEntity = base.Entity;
				return baseEntity;
			}
		}
		#endregion Filling entity fields with viewmodels-fields entity values

		#region References
		public System.String Name
		{
			get { return Entity.Name; }
			set { SetProperty(Entity, e => e.Name, value); }
		}

		public System.String Description
		{
			get { return Entity.Description; }
			set { SetProperty(Entity, e => e.Description, value); }
		}

		public System.Uri DesciptionUrl
		{
			get { return Entity.DesciptionUrl; }
			set { SetProperty(Entity, e => e.DesciptionUrl, value); }
		}

		#endregion References

		#endregion Properties
	}

	public partial class TestViewModel : BaseApiViewModel<ApiLibrary.ApiEntities.Metadata.Test>
	{
		#region Constructors
		public TestViewModel() : base()
		{
			
		}
		public TestViewModel(ApiLibrary.ApiEntities.Metadata.Test originalEntity) : base(originalEntity)
		{
			
		}
		#endregion Constructors

		#region Properties

		#region Filling entity fields with viewmodels-fields entity values
		public override ApiLibrary.ApiEntities.Metadata.Test Entity
		{
			get
			{
				var baseEntity = base.Entity;
				return baseEntity;
			}
		}
		#endregion Filling entity fields with viewmodels-fields entity values

		#region References
		public System.Int32 Id
		{
			get { return Entity.Id; }
			set { SetProperty(Entity, e => e.Id, value); }
		}

		public System.String Name
		{
			get { return Entity.Name; }
			set { SetProperty(Entity, e => e.Name, value); }
		}

		public System.Int32 Weight
		{
			get { return Entity.Weight; }
			set { SetProperty(Entity, e => e.Weight, value); }
		}

		#endregion References

		#endregion Properties
	}

}

namespace Downwork_Notifier.ViewModels.API.ApiEntities.Profiles
{
	public partial class ClientViewModel : BaseApiViewModel<ApiLibrary.ApiEntities.Profiles.Client>
	{
		#region Constructors
		public ClientViewModel() : base()
		{
			
		}
		public ClientViewModel(ApiLibrary.ApiEntities.Profiles.Client originalEntity) : base(originalEntity)
		{
			
		}
		#endregion Constructors

		#region Properties

		#region Filling entity fields with viewmodels-fields entity values
		public override ApiLibrary.ApiEntities.Profiles.Client Entity
		{
			get
			{
				var baseEntity = base.Entity;
				return baseEntity;
			}
		}
		#endregion Filling entity fields with viewmodels-fields entity values

		#region References
		public System.String Country
		{
			get { return Entity.Country; }
			set { SetProperty(Entity, e => e.Country, value); }
		}

		public System.Double Feedback
		{
			get { return Entity.Feedback; }
			set { SetProperty(Entity, e => e.Feedback, value); }
		}

		public System.Int32 JobsPosted
		{
			get { return Entity.JobsPosted; }
			set { SetProperty(Entity, e => e.JobsPosted, value); }
		}

		public System.Int32 PastHires
		{
			get { return Entity.PastHires; }
			set { SetProperty(Entity, e => e.PastHires, value); }
		}

		public System.String PaymentVerificationStatus
		{
			get { return Entity.PaymentVerificationStatus; }
			set { SetProperty(Entity, e => e.PaymentVerificationStatus, value); }
		}

		public System.Int32 ReviewsCount
		{
			get { return Entity.ReviewsCount; }
			set { SetProperty(Entity, e => e.ReviewsCount, value); }
		}

		#endregion References

		#endregion Properties
	}

	public partial class JobViewModel : BaseApiViewModel<ApiLibrary.ApiEntities.Profiles.Job>
	{
		#region Constructors
		public JobViewModel() : base()
		{
			Skills = new ObservableCollection<System.String>();
			Client = new ClientViewModel();
			
		}
		public JobViewModel(ApiLibrary.ApiEntities.Profiles.Job originalEntity) : base(originalEntity)
		{
			Skills = new ObservableCollection<System.String>(originalEntity.Skills ?? new System.String[0]);
			Client = new ClientViewModel(originalEntity.Client);
			
		}
		#endregion Constructors

		#region Properties

		#region Filling entity fields with viewmodels-fields entity values
		public override ApiLibrary.ApiEntities.Profiles.Job Entity
		{
			get
			{
				var baseEntity = base.Entity;
				baseEntity.Client = Client.Entity;
				return baseEntity;
			}
		}
		#endregion Filling entity fields with viewmodels-fields entity values

		#region Collections
		public ObservableCollection<System.String> Skills
		{
			get { return _Skills; }
			set { SetProperty(ref _Skills, value); }
		}
		private ObservableCollection<System.String> _Skills = new ObservableCollection<System.String>();
		#endregion Collections

		#region References
		public System.String Id
		{
			get { return Entity.Id; }
			set { SetProperty(Entity, e => e.Id, value); }
		}

		public System.String Title
		{
			get { return Entity.Title; }
			set { SetProperty(Entity, e => e.Title, value); }
		}

		public System.DateTime DateCreated
		{
			get { return Entity.DateCreated; }
			set { SetProperty(Entity, e => e.DateCreated, value); }
		}

		public System.String Snippet
		{
			get { return Entity.Snippet; }
			set { SetProperty(Entity, e => e.Snippet, value); }
		}

		public System.Uri Url
		{
			get { return Entity.Url; }
			set { SetProperty(Entity, e => e.Url, value); }
		}

		public System.String Category
		{
			get { return Entity.Category; }
			set { SetProperty(Entity, e => e.Category, value); }
		}

		public System.String Subcategory
		{
			get { return Entity.Subcategory; }
			set { SetProperty(Entity, e => e.Subcategory, value); }
		}

		public ApiLibrary.ApiEntities.Profiles.JobType JobType
		{
			get { return Entity.JobType; }
			set { SetProperty(Entity, e => e.JobType, value); }
		}

		public System.String JobStatus
		{
			get { return Entity.JobStatus; }
			set { SetProperty(Entity, e => e.JobStatus, value); }
		}

		public System.Int32 Budget
		{
			get { return Entity.Budget; }
			set { SetProperty(Entity, e => e.Budget, value); }
		}

		public System.String Workload
		{
			get { return Entity.Workload; }
			set { SetProperty(Entity, e => e.Workload, value); }
		}

		public System.String Duration
		{
			get { return Entity.Duration; }
			set { SetProperty(Entity, e => e.Duration, value); }
		}

		public ClientViewModel Client
		{
			get { return _Client; }
			set { SetProperty(ref _Client, value); }
		}
		private ClientViewModel _Client;

		#endregion References

		#endregion Properties
	}

}

namespace Downwork_Notifier.ViewModels.API.ApiModules.RequestParameters
{
	public partial class JobListParametersViewModel : BaseApiViewModel<ApiLibrary.ApiModules.RequestParameters.JobListParameters>
	{
		#region Constructors
		public JobListParametersViewModel() : base()
		{
			Paging = new PagingViewModel();
			
		}
		public JobListParametersViewModel(ApiLibrary.ApiModules.RequestParameters.JobListParameters originalEntity) : base(originalEntity)
		{
			Paging = new PagingViewModel(originalEntity.Paging);
			
		}
		#endregion Constructors

		#region Properties

		#region Filling entity fields with viewmodels-fields entity values
		public override ApiLibrary.ApiModules.RequestParameters.JobListParameters Entity
		{
			get
			{
				var baseEntity = base.Entity;
				baseEntity.Paging = Paging.Entity;
				return baseEntity;
			}
		}
		#endregion Filling entity fields with viewmodels-fields entity values

		#region References
		public System.String BuyerTeamReference
		{
			get { return Entity.BuyerTeamReference; }
			set { SetProperty(Entity, e => e.BuyerTeamReference, value); }
		}

		public System.Int32 IncludeSubTeams
		{
			get { return Entity.IncludeSubTeams; }
			set { SetProperty(Entity, e => e.IncludeSubTeams, value); }
		}

		public System.String CreatedBy
		{
			get { return Entity.CreatedBy; }
			set { SetProperty(Entity, e => e.CreatedBy, value); }
		}

		public System.String Status
		{
			get { return Entity.Status; }
			set { SetProperty(Entity, e => e.Status, value); }
		}

		public System.DateTime CreatedAfter
		{
			get { return Entity.CreatedAfter; }
			set { SetProperty(Entity, e => e.CreatedAfter, value); }
		}

		public System.DateTime CreatedBefore
		{
			get { return Entity.CreatedBefore; }
			set { SetProperty(Entity, e => e.CreatedBefore, value); }
		}

		public PagingViewModel Paging
		{
			get { return _Paging; }
			set { SetProperty(ref _Paging, value); }
		}
		private PagingViewModel _Paging;

		public System.String OrderBy
		{
			get { return Entity.OrderBy; }
			set { SetProperty(Entity, e => e.OrderBy, value); }
		}

		#endregion References

		#endregion Properties
	}

	public partial class JobSearchParametersViewModel : BaseApiViewModel<ApiLibrary.ApiModules.RequestParameters.JobSearchParameters>
	{
		#region Constructors
		public JobSearchParametersViewModel() : base()
		{
			Budget = new RangeParameterViewModel<Int32>();
			ClientFeedback = new RangeParameterViewModel<Double>();
			ClientHires = new RangeParameterViewModel<Int32>();
			Paging = new PagingViewModel();
			
		}
		public JobSearchParametersViewModel(ApiLibrary.ApiModules.RequestParameters.JobSearchParameters originalEntity) : base(originalEntity)
		{
			Budget = new RangeParameterViewModel<Int32>(originalEntity.Budget);
			ClientFeedback = new RangeParameterViewModel<Double>(originalEntity.ClientFeedback);
			ClientHires = new RangeParameterViewModel<Int32>(originalEntity.ClientHires);
			Paging = new PagingViewModel(originalEntity.Paging);
			
		}
		#endregion Constructors

		#region Properties

		#region Filling entity fields with viewmodels-fields entity values
		public override ApiLibrary.ApiModules.RequestParameters.JobSearchParameters Entity
		{
			get
			{
				var baseEntity = base.Entity;
				baseEntity.Budget = Budget.Entity;
				baseEntity.ClientFeedback = ClientFeedback.Entity;
				baseEntity.ClientHires = ClientHires.Entity;
				baseEntity.Paging = Paging.Entity;
				return baseEntity;
			}
		}
		#endregion Filling entity fields with viewmodels-fields entity values

		#region References
		public System.String Query
		{
			get { return Entity.Query; }
			set { SetProperty(Entity, e => e.Query, value); }
		}

		public System.String Title
		{
			get { return Entity.Title; }
			set { SetProperty(Entity, e => e.Title, value); }
		}

		public System.String Skills
		{
			get { return Entity.Skills; }
			set { SetProperty(Entity, e => e.Skills, value); }
		}

		public System.String Category
		{
			get { return Entity.Category; }
			set { SetProperty(Entity, e => e.Category, value); }
		}

		public System.String Subcategory
		{
			get { return Entity.Subcategory; }
			set { SetProperty(Entity, e => e.Subcategory, value); }
		}

		public ApiLibrary.ApiEntities.Const.JobType JobType
		{
			get { return Entity.JobType; }
			set { SetProperty(Entity, e => e.JobType, value); }
		}

		public ApiLibrary.ApiEntities.Const.JobStatus JobStatus
		{
			get { return Entity.JobStatus; }
			set { SetProperty(Entity, e => e.JobStatus, value); }
		}

		public RangeParameterViewModel<Int32> Budget
		{
			get { return _Budget; }
			set { SetProperty(ref _Budget, value); }
		}
		private RangeParameterViewModel<Int32> _Budget;

		public ApiLibrary.ApiEntities.Const.Workload Workload
		{
			get { return Entity.Workload; }
			set { SetProperty(Entity, e => e.Workload, value); }
		}

		public ApiLibrary.ApiEntities.Const.Duration Duration
		{
			get { return Entity.Duration; }
			set { SetProperty(Entity, e => e.Duration, value); }
		}

		public System.Int32 DaysPosted
		{
			get { return Entity.DaysPosted; }
			set { SetProperty(Entity, e => e.DaysPosted, value); }
		}

		public RangeParameterViewModel<Double> ClientFeedback
		{
			get { return _ClientFeedback; }
			set { SetProperty(ref _ClientFeedback, value); }
		}
		private RangeParameterViewModel<Double> _ClientFeedback;

		public RangeParameterViewModel<Int32> ClientHires
		{
			get { return _ClientHires; }
			set { SetProperty(ref _ClientHires, value); }
		}
		private RangeParameterViewModel<Int32> _ClientHires;

		public PagingViewModel Paging
		{
			get { return _Paging; }
			set { SetProperty(ref _Paging, value); }
		}
		private PagingViewModel _Paging;

		public System.String Sort
		{
			get { return Entity.Sort; }
			set { SetProperty(Entity, e => e.Sort, value); }
		}

		#endregion References

		#endregion Properties
	}

	public partial class PagingViewModel : BaseApiViewModel<ApiLibrary.ApiModules.RequestParameters.Paging>
	{
		#region Constructors
		public PagingViewModel() : base()
		{
			
		}
		public PagingViewModel(ApiLibrary.ApiModules.RequestParameters.Paging originalEntity) : base(originalEntity)
		{
			
		}
		#endregion Constructors

		#region Properties

		#region Filling entity fields with viewmodels-fields entity values
		public override ApiLibrary.ApiModules.RequestParameters.Paging Entity
		{
			get
			{
				var baseEntity = base.Entity;
				return baseEntity;
			}
		}
		#endregion Filling entity fields with viewmodels-fields entity values

		#region References
		public System.Int64 Offset
		{
			get { return Entity.Offset; }
			set { SetProperty(Entity, e => e.Offset, value); }
		}

		public System.Int32 Count
		{
			get { return Entity.Count; }
			set { SetProperty(Entity, e => e.Count, value); }
		}

		public System.Int64 Total
		{
			get { return Entity.Total; }
			set { SetProperty(Entity, e => e.Total, value); }
		}

		#endregion References

		#endregion Properties
	}

	public partial class RangeParameterViewModel<TValue> : BaseApiViewModel<ApiLibrary.ApiModules.RequestParameters.RangeParameter<TValue>> where TValue : struct
	{
		#region Constructors
		public RangeParameterViewModel() : base()
		{
			
		}
		public RangeParameterViewModel(ApiLibrary.ApiModules.RequestParameters.RangeParameter<TValue> originalEntity) : base(originalEntity)
		{
			
		}
		#endregion Constructors

		#region Properties

		#region Filling entity fields with viewmodels-fields entity values
		public override ApiLibrary.ApiModules.RequestParameters.RangeParameter<TValue> Entity
		{
			get
			{
				var baseEntity = base.Entity;
				return baseEntity;
			}
		}
		#endregion Filling entity fields with viewmodels-fields entity values

		#region References
		public System.Nullable<TValue> Min
		{
			get { return Entity.Min; }
			set { SetProperty(Entity, e => e.Min, value); }
		}

		public System.Nullable<TValue> Max
		{
			get { return Entity.Max; }
			set { SetProperty(Entity, e => e.Max, value); }
		}

		#endregion References

		#endregion Properties
	}

	public partial class RangeViewModel<TClass> : BaseApiViewModel<ApiLibrary.ApiModules.RequestParameters.Range<TClass>> where TClass : class
	{
		#region Constructors
		public RangeViewModel() : base()
		{
			
		}
		public RangeViewModel(ApiLibrary.ApiModules.RequestParameters.Range<TClass> originalEntity) : base(originalEntity)
		{
			
		}
		#endregion Constructors

		#region Properties

		#region Filling entity fields with viewmodels-fields entity values
		public override ApiLibrary.ApiModules.RequestParameters.Range<TClass> Entity
		{
			get
			{
				var baseEntity = base.Entity;
				return baseEntity;
			}
		}
		#endregion Filling entity fields with viewmodels-fields entity values

		#region References
		public TClass Min
		{
			get { return Entity.Min; }
			set { SetProperty(Entity, e => e.Min, value); }
		}

		public TClass Max
		{
			get { return Entity.Max; }
			set { SetProperty(Entity, e => e.Max, value); }
		}

		#endregion References

		#endregion Properties
	}

}