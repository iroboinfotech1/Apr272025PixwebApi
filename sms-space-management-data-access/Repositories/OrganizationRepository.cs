
using Dapper;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.data.access.BusinessLogic;
using sms.space.management.domain.Entities.Organization;

namespace sms.space.management.data.access.Repositories
{
	public class CountryRepository : ICountryRepository
	{
		private readonly DbSession _session;
		public CountryRepository(DbSession session)
		{
			_session = session;
		}

		public async Task<List<Country>> GetAll()
		{
			var query = "Select * from public.countries";
			var result = await _session.Connection.QueryAsync<Country>(query, null, _session.Transaction);
			return result.ToList();
		}
	}

	public class StateRepository : IStateRepository
	{
		private readonly DbSession _session;
		public StateRepository(DbSession session)
		{
			_session = session;
		}

		public async Task<List<StateEntity>> GetByCountryId(int id)
		{
			var query = "Select * from public.states where country_id=@Id";
			var result = await _session.Connection.QueryAsync<StateEntity>(query, new { Id = id }, _session.Transaction);
			return result.ToList();
		}
	}

	public class CityRepository : ICityRepository
	{
		private readonly DbSession _session;
		public CityRepository(DbSession session)
		{
			_session = session;
		}

		public async Task<List<CityEntity>> GetByStateId(int id)
		{
			var query = "Select * from public.cities where state_id=@Id";
			var result = await _session.Connection.QueryAsync<CityEntity>(query, new { Id = id }, _session.Transaction);
			return result.ToList();
		}
	}

	public class OrganizationRepository : IOrganizationRepository
	{
		private readonly DbSession _session;

		public OrganizationRepository(DbSession session)
		{
			_session = session;
		}

		public async Task<Organization> Create(Organization entity)
		{
			var query = $@"INSERT INTO space_admin.organisation_master(
							org_guid, org_name, industry, building_name, mailing_address, country, state, city, zipcode, phone_number, email, website, image, logo)
							VALUES (@OrgGuid,@OrgName,@Industry,@BuildingName,@MailingAddress,@Country,@State,@City,@Zipcode,@PhoneNumber,@Email,@Website,@Image,@Logo)
							RETURNING org_id
							";
			//RETURNING id


			entity.OrgId = await _session.Connection.ExecuteScalarAsync<int>(query, entity, _session.Transaction);



			return entity;
		}

		public async Task<bool> Delete(int id)
		{

			var query = "Delete from space_admin.organisation_master where org_id=@ID";

			int isDeleted = await _session.Connection.ExecuteAsync(query, new { ID = id }, _session.Transaction);

			if (isDeleted > 0)
				return true;
			else
				return false;
		}

		//public async Task<Organization> Get()
		//{
		//    var connectionString = "Host=localhost:5432; Database=SpaceManagement; Username=smsdev;Password=Password*1";
		//    var connection = new NpgsqlConnection(connectionString);
		//    connection.Open();
		//    var query = "SELECT * FROM space_admin.organisation_master";
		//    return await connection.QueryFirstOrDefaultAsync<Organization>(query);

		//}

		public async Task<List<Organization>> GetAll()
		{
			var query = $@"SELECT 
							co.name as CountryName, co.id as CountryId,
							s.name as StateName, s.id as StateId,
							ci.name as CityName, ci.id as CityId,
							o.*
							FROM space_admin.organisation_master o
							LEFT JOIN public.countries co ON o.country = co.id
							LEFT JOIN public.states s ON o.state = s.id
							LEFT JOIN public.cities ci ON o.city = ci.id
							";
			var result = await _session.Connection.QueryAsync<Organization>(query, null, _session.Transaction);
			return result.ToList();
		}

		public async Task<Organization?> GetById(int id)
		{
			var query = $@"SELECT 
							co.name as CountryName, co.id as CountryId,
							s.name as StateName, s.id as StateId,
							ci.name as CityName, ci.id as CityId,
							o.*
							FROM space_admin.organisation_master o
							LEFT JOIN public.countries co ON cast(o.country as int) = co.id
							LEFT JOIN public.states s ON cast(o.state as int) = s.id
							LEFT JOIN public.cities ci ON cast(o.city as int) = ci.id
							where org_id=@Id";

			var result = await _session.Connection.QueryAsync<Organization>(query, new { Id = id }, _session.Transaction);
			return result.FirstOrDefault();
		}

		public async Task<bool> Update(Organization entity)
		{
			//update query
			var query = $@"Update space_admin.organisation_master set 
							org_name = @OrgName, industry = @Industry,
							building_name=@BuildingName,mailing_address=@MailingAddress,country=@Country,
							state=@State,city=@City,zipcode=@Zipcode,
							phone_number = @PhoneNumber, email = @Email, website = @Website,
							image=@Image,logo=@Logo 
							where org_id = @OrgId";

			var result = await _session.Connection.ExecuteAsync(query, entity, _session.Transaction);

			if (result > 0)
				return true;
			else
				return false;
		}
	}
}
