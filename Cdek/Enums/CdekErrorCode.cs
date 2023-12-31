﻿namespace XyloCode.ThirdPartyServices.Cdek.Enums
{
    public enum CdekErrorCode
    {
        v2_internal_error,
        v2_similar_request_still_processed,
        v2_bad_request,
        v2_invalid_format,
        v2_field_is_empty,
        v2_parameters_empty,
        v2_invalid_value_type,
        v2_entity_not_found,
        v2_entity_forbidden,
        v2_entity_invalid,
        v2_order_not_found,
        v2_order_forbidden,
        v2_order_number_empty,
        v2_shipment_address_multivalued,
        v2_delivery_address_multivalued,
        v2_sender_location_not_recognized,
        v2_recipient_location_not_recognized,
        v2_number_items_is_more_126,
        orders_number_is_empty,
        shipment_location_is_not_recognized,
        delivery_location_is_not_recognized,
        v2_entity_not_found_im_number,
        v2_order_location_from_and_shipment_point_empty,
        v2_order_location_to_and_delivery_point_empty,
        v2_package_id_is_empty,
        v2_required_param_empty,
        v2_city_can_not_be_changed,
        v2_impossible_register_date,
        v2_intake_exists_by_order,
        v2_intake_exists_by_date_address,
        v2_webhook_type_incorrect,
        v2_entity_empty,
        v2_wrong_interval,
        v2_entity_expired,
        v2_unable_add_service,
        v2_order_item_amount_incorrect,
        error_validate_order_cash_on_delivery_currency_invalid,
        error_validate_order_cash_on_delivery_currency_not_equals_declared_cost,
        error_validate_declare_cost_currency_not_equals_payer_currency,
        error_validate_method_of_payment_receiver_city_limit_payment_exceeded,
        error_validate_courier_invitation_time_expired,
        error_validate_courier_invitation_interval_too_small,
        error_validate_courier_invitation_time_too_small,
        error_validate_courier_invitation_time_too_large,
        ve_package_weight_too_small,
        ve_package_weight_too_large,
        ve_currency_not_corresponds_to_contract,
        ve_currency_not_corresponds_to_contragent,
        ve_currency_not_corresponds_to_city,
        error_validate_sender_name_incorrect_symbol,
        error_validate_sender_name_only_digits,
        error_validate_receiver_name_incorrect_symbol,
        error_validate_receiver_name_only_digits,
        error_validate_receiver_city_cash_limit,
        ve_receiver_phone_type_incorrect,
        ve_receiver_pickup_point_empty,
        error_validate_receiver_delivery_point_calc_weight_small,
        error_validate_receiver_delivery_point_calc_weight_large,
        ve_eceiver_pickup_point_take_only,
        error_validate_package_number_not_unique,
        error_validate_package_one_of_size_is_zero,
        error_validate_package_count_to_pochtomat,
        error_validate_good_article_is_not_unique,
        error_validate_additional_service_not_available,
        ve_as_forbidden,
        error_validate_additional_service_param_declared_cost_is_required,
        error_validate_additional_service_param_unsupported_param,
        error_internal_cannot_set_vat,
        error_validate_package_article_not_unique,
        error_calculator_calculate,
        error_validate_additional_service_param_declared_cost_is_does_not_match_with_goods,
        jv_the_number_of_orders_at_the_request_of_more_than_1000,
        error_validate_dd_date_delivery_before_order_date,
        ve_as_reverse_only_to_door,
        ve_as_reverse_contract_forbidden,
        ve_as_reverse_sender_address_empty,
        error_validate_package_im_wrong_amount,
        error_validate_package_number_has_alreasy_used,
        error_validate_good_vat_rate_diff,
        error_validate_good_vat_rate_is_not_selected,
        error_validate_good_vat_rate_summ_empty,
        error_validate_good_vat_rate_summ_not_appropriate,
        error_validate_good_article_has_already,
        error_validate_good_payment_not_suite,
        error_validate_online_shop_price_threshold_can_not_be_duplicate,
        error_validate_online_shop_price_threshold_surcharge_vat_sum_incorrect,
        error_dsd_validate_vat_sum_difference_between,
        error_validate_online_shop_price_threshold_cost_less_min_value,
        error_dsd_validate_vat_rate_code_incorrect,
        ve_receiver_pickup_point_wrong_city,
        error_validate_declared_cost_of_insurance_not_eq_to_dc_of_goods,
        error_validate_good_gros_weight_less_than_net_weight,
        error_validate_im_dep_number_has_already_had_integration,
        error_validate_online_shop_seller_ownership_form_incorrect,
        error_validate_good_label_count_is_over_limit,
        error_validate_order_delete_status,
        error_validate_order_delete_documents_in_warehouse_is_not_empty,
        error_validate_order_delete_claims_is_not_empty,
        error_validate_postamat_package_count,
        error_validate_postamat_package_weight,
        error_validate_postamat_pvz_not_postamat,
        error_validate_delivery_detail_postamat_part_delivery,
        error_internal_reverse_overweight,
        error_validate_reverse_deny_change_sender_city,
        error_validate_reverse_deny_change_sender_address,
        error_validate_build_ci_from_order,
        ve_ci_pickup_date_expired,
        ve_ci_pickup_time_too_small,
        ve_ci_pickup_time_too_large,
        ve_ci_pickup_time_selfcare_too_large,
        ve_ci_pickup_time_to_before_from,
        ve_ci_pickup_lunch_too_small,
        ve_ci_pickup_lunch_too_large,
        ve_ci_pickup_lunch_interval_incorrect,
        ve_ci_pickup_lunch_interval_zero,
        ve_ci_pickup_date_too_large,
        ve_ci_status_for_change_date_incorrect,
        ve_tariff_mode_not_from_door,
        ve_tariff_mode_warehouse_unavailable,
        ve_delivery_time_empty,
        ve_receiver_pickup_point_incorrect,
        ve_payer_contract_status_incorrect,
        error_validate_online_shop_number_departure_is_not_unique,
        ve_as_part_deliv_count,
        ve_as_part_deliv_forbiden,
        ve_as_get_up_floor_any_checked,
        error_validate_tariff_not_supported_for_this_drirection,
        error_validate_additional_service_param_cant_part_deliv_one_good,
        ve_reverse_straight_order_not_found,
        ie_reverse_mode_not_supported,
        ve_reverse_tariff_not_available,
        err_result_service_empty,
        err_result_servicelist_empty,
        err_addservice_getup_floor_elevator,
        err_invalid_tariff_with_datetimeordersend,
        err_invalid_tariff_with_ordertype,
        err_invalid_tariff_with_dimentions,
        err_pvz_with_tariff_mistake,
        ERR_PVZ_WEIGHT_LIMIT,
        ve_calc_as_order_type,
        error_validate_good_invalid_label,
        ve_calc_as_not_compatible,
        err_addservice_ban_attachment_inspection_incompatible_part_deliv,
        err_addservice_ban_attachment_inspection_incompatible_trying_on,
        error_validate_goods_label_count_is_over_limit,
        code_invalid,
        PA_02,
        PA_17,
        PA_11,
        unauthorized,
    }
}
