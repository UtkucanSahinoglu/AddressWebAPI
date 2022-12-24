<template>
  <div>
    <table class="ShowDetail">
      <thead>
        <tr>
          <th>Adi</th>
          <th>Soyadi</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td>{{ user.firstName }}</td>
          <td>{{ user.lastName }}</td>
        </tr>
      </tbody>
    </table>
    <table class="showAddress">
      <thead>
        <tr>
          <th>Adres Adi</th>
          <th>Açik Adres</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="address in addresses" :key="address.userId">
          <td v-text="address.name"></td>
          <td v-text="address.openAddress"></td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
//https://localhost:5001/Address/GetByUserId?UserId=2
//https://localhost:5001/User/GetByIdUser?id=2
const baseUrl = "https://localhost:5001";

import Vue from "vue";
import axios from "axios";
import VueAxios from "vue-axios";

Vue.use(VueAxios, axios);

export default {
  name: "ShowComponent",
  props: { user: { type: Object }, address: { type: Object } },
  data() {
    return {
      addresses: [],
      userId: this.user.id,
    };
  },
  methods: {
    GetUserId(id) {
      axios
        .get(baseUrl + "/User/GetByIdUser?Id=" + id)
        .then((response) => {
          this.users = response.data;
          console.log(response);
        })
        .catch((err) => {
          console.log(err);
        });
    },
    GetAddressUserId() {
      axios
        .get(baseUrl + "/Address/GetByUserId?UserId=" + this.userId)
        .then((response) => {
          this.addresses = response.data;
          console.log(response);
        })
        .catch((err) => {
          console.log(err);
        });
    },
  },
  mounted() {
    console.log(this.user);
    this.GetAddressUserId();
    console.log(this.userId);
  },
};
</script>

<style>

</style>
