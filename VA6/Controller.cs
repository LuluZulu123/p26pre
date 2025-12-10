namespace VA6;

public class Controller
{
    private static void Load (string cmd) {
        DB.VA6.Connector.GetResults<Unit>(cmd);
        // DB.VA6.Connector.GetResults<Unit>(@"SELECT * FROM unit");
    }
	public static void GetProfiles () {}
	public static void GetUnits () {}
	public static void GetItems () {}
	public static void GetMatches () {}
	public static void GetMap () {}
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
	public static void SellUnit (uint unitId) {
    }
}