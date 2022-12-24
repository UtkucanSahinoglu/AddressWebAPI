<template>
  <div>
    <form @submit="updateUserData" method="put">
      <label class="Firstname" for="FirstName">Adi</label><br />
      <input
        class="FirstName"
        type="text"
        name="firstName"
        placeholder="Adınızı giriniz"
        required
        v-model="putUser.firstName"
      />
      <br />
      <br />
      <label class="LastName" for="LastName">Soyadi</label><br />
      <input
        class="LastName"
        type="text"
        name="lastName"
        placeholder="Soyadınızı giriniz"
        required
        v-model="putUser.lastName"
      />
      <br />
      <br />
      <label class="Name" for="Name">Adres Adi</label><br />
      <input
        class="Name"
        type="text"
        name="Name"
        placeholder="Adres adını giriniz"
        v-model="putAddress.name"
      />
      <br />
      <br />
      <label class="OpenAddress" for="OpenAddress">Açik adres</label><br />
      <input
        class="OpenAddress"
        type="text"
        name="OpenAddress"
        placeholder="Açık adresinizi giriniz"
        v-model="putAddress.openAddress"
      />
      <br />
      <br />
      <b-button class="AddressAdd" @click="AddAddress" type="button">
        Adres Güncelle
      </b-button>
      <ul>
        <table class="AddAddressTable">
          <thead>
            <tr>
              <th>Name</th>
              <th>OpenAddress</th>
            </tr>
            <tr v-for="address in putUser.addresses" :key="address.id">
              <td v-text="address.name"></td>
              <td v-text="address.openAddress"></td>
              <b-button
                class="AddressDelete"
                @click="DeleteAddressById(address.id)"
              >
                Sil
              </b-button>
            </tr>
          </thead>
        </table>
      </ul>
      <b-button class="UserAdd" type="submit">User Güncelle</b-button>
    </form>
  </div>
</template>

<script>
const baseUrl = "https://localhost:5001";

import Vue from "vue";
import axios from "axios";
import VueAxios from "vue-axios";
import Swal from "sweetalert2/dist/sweetalert2.js";

Vue.use(VueAxios, axios);

export default {
  name: "UpdateComponent",
  props: { user: { type: Object } },
  data() {
    return {
      addresses: [],
      userId: this.user.id,
      putUser: {
        id: null,
        firstName: null,
        lastName: null,
        addresses: [],
      },
      putAddress: {
        id: null,
        userId: null,
        name: null,
        openAddress: null,
      },
    };
  },
  mounted() {
    this.GetUserById(this.user.id);
    this.GetAddressByUserId();
    //console.log(this.user);
    //console.log(this.userId);
    console.log(this.putUser);
    console.log(this.putAddress);
    console.log(this.addresses);
  },
  methods: {
    DeleteAddressById(id) {
      Swal.fire({
        title: "Silmek istediğine eminmisin?",
        text: "Bu işlem adresi kalici olarak siler",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Sil",
        cancelButtonText: "İptal Et",
      }).then((result) => {
        if (result.isConfirmed) {
          this.axios
            .delete(baseUrl + "/Address/DeleteByAddressId?Id=" + id)
            .then((result) => {
              this.GetUserById(this.user.id);
              console.log(result);
            })
            .catch((err) => {
              console.log(err);
            });
          Swal.fire("Adres silindi", "İşlem Başarılı");
        }
      });
    },

    GetUserById(id) {
      axios
        .get(baseUrl + "/User/GetByIdUser?Id=" + id)
        .then((response) => {
          this.users = response.data;
          this.putUser.id = response.data.id;
          this.putUser.firstName = response.data.firstName;
          this.putUser.lastName = response.data.lastName;
          this.putUser.addresses = response.data.addresses;

          console.log(response);
        })
        .catch((err) => {
          console.log(err);
        });
    },
    GetAddressByUserId() {
      axios
        .get(baseUrl + "/Address/GetByUserId?UserId=" + this.userId)
        .then((response) => {
          this.putAddress.id = response.data.id;
          this.putAddress.userId = response.data.userId;
          this.putAddress.name = response.data.name;
          this.putAddress.openAddress = response.data.openAddress;
          // this.addresses = response.data;
          console.log(response);
        })
        .catch((err) => {
          console.log(err);
        });
    },
    updateUserData(e) {
      e.preventDefault();
      if (this.putUser.addresses.length == 0) {
        this.$swal({
          icon: "error",
          title: "Ooops..",
          text: "Kullanıcı güncellemek için en az bir tane adres tanımı yapmanız gerekmektedir!",
        });
        return;
      }
      Swal.fire("Kullanici güncellendi");
      console.log(this.putAddress);
      axios
        .put("https://localhost:5001/User/UpdateUser", this.putUser)
        .then((result) => {
          window.location.reload()
          console.warn(result);
        });
      this.$bvModal.hide("updateuser");
      e.preventDefault();
    },
    AddAddress(e) {
      e.preventDefault();
      const exists = this.putUser.addresses.filter(
        (x) => x.name === this.putAddress.name
      );
      if (exists && exists.length > 0) {
        this.$swal({
          icon: "error",
          title: "Ooops..",
          text: "Eklemeye çalıştığınız adres ismi mevcut! Lütfen farklı isim deneyiniz!",
        });
        return;
      }
      this.putUser.addresses.push({
        name: this.putAddress.name,
        openAddress: this.putAddress.openAddress,
      });
      this.postsAddress.name = null;
      this.postsAddress.openAddress = null;
    },

    updateUser() {
      this.putUser.push({
        id: this.users.id,
        firstName: this.users.firstName,
        lastName: this.users.lastName,
      });
      this.users.id = null;
      this.users.firstName = null;
      this.users.lastName = null;

      this.putUser.addresses.push({
        id: this.addresses.id,
        userId: this.addresses.userId,
        name: this.addresses.name,
        openAddress: this.addresses.openAddress,
      });
      this.addresses.id = null;
      this.addresses.userId = null;
      this.addresses.name = null;
      this.addresses.openAddress = null;
    },
    updateAddress() {
      this.putUser.addresses.push({
        id: this.addresses.id,
        userId: this.addresses.userId,
        name: this.addresses.name,
        openAddress: this.addresses.openAddress,
      });
      this.addresses.id = null;
      this.addresses.userId = null;
      this.addresses.name = null;
      this.addresses.openAddress = null;
    },
    removeAddress: function (index) {
      console.log("Removing", index);
      this.postsUser.addresses.splice(index, 1);
    },
  },
};
</script>

<style>
label.Firstname {
  margin-top: 0px;
  margin-right: 390px;
}
input.FirstName {
  position: absolute;
  margin-top: 10px;
  margin-left: -208px;
}
label.LastName {
  margin-top: 0px;
  margin-right: 365px;
}
input.LastName {
  position: absolute;
  margin-top: 10px;
  margin-left: -208px;
}
label.Name {
  position: absolute;
  margin-top: -140px;
  margin-left: 20px;
}
input.Name {
  position: absolute;
  margin-top: -134px;
  margin-left: 20px;
}
label.OpenAddress {
  position: absolute;
  margin-top: -140px;
  margin-left: 20px;
}
input.OpenAddress {
  position: absolute;
  margin-top: -134px;
  margin-left: 20px;
}
</style>
