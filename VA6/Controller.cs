using System.Text.Json.Serialization;
using DB;
using Extensions;
using Newtonsoft.Json;
using Handler = Handlers.VA6;

namespace VA6;

public class Controller
{
	public static async Task GetProfiles () {}
	public static async Task GetUnits (Handler handler) {
        if (handler.ProfileId == 0)
            return;
        
		using var conn = await ConnectorBase.GetConnection("va6");
		
		var deb2 = conn.LoadGeneric(@"SELECT 
                                        u.uid, 
                                        u.profile_uid, 
                                        u.start_hp, 
                                        u.hp, 
                                        u.class, 
                                        u.class_hp, 
                                        u.match_uid, 
                                        u.alive, 
                                        u.modify_time, 
                                        u.unit_number, 
                                        u.delete_time, 
                                        u.sell_time, 
                                        u.temp_hp_change, 
                                        u.xp, 
                                        u.max_xp, 
                                        u.max_hp, 
                                        u.gear_score, 
                                        u.def, 
                                        u.att, 
                                        u.lvl, 
                                        u.revive_cost, 
                                        u.heal_cost, 
                                        u.sell_price, 
                                        u.buy_price
                                    FROM
                                        va6.v_unit u
                                    WHERE
                                        u.profile_uid = @profileId
        ;", (x) => new {
            Uid = x["uid"],
            Class = x["class"],
            Hp = x["hp"],
            MaxHp = x["max_hp"],
            Xp = x["xp"],
            MaxXp = x["max_xp"],
            GearScore = x["gear_score"],
		}, ("profileId", handler.ProfileId));
		
		await foreach (var row in deb2)
		{
            await handler.Send(JsonConvert.SerializeObject(row));
		}
		
		await conn.CloseAsync();
    }
	public static async Task GetItems () {}
	public static async Task GetMatches () {}
	public static async Task GetMap () {}
	public static async Task SellUnit (uint unitId)
	{
	}
    // level_from_xp
    // max_xp_from_level
    // p_add_item
    // p_add_unit
    // p_attack__melee
    // p_buy_item
    // p_buy_unit
    // p_calc_autolog
    // p_calc_fx
    // p_cpu_join_match
    // p_create_account
    // p_create_profile
    // p_end_turn
    // p_equip_item
    // p_finish_match
    // p_get_available_units
    // p_get_items
    // p_get_map
    // p_get_maps
    // p_get_match_log
    // p_get_match_units
    // p_get_matches
    // p_get_profiles
    // p_get_shop_items
    // p_get_shop_units
    // p_get_units
    // p_heal_unit
    // p_prepare_data
    // p_prepare_data__item
    // p_prepare_data__match
    // p_prepare_data__profile
    // p_prepare_data__unit
    // p_revive_unit
    // p_sell_unit
    // p_start_match
}