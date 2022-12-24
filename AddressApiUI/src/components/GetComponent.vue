<template>
  <div>
    <h3 class="UserList">Kullanici Listesi</h3>
    <table class="GetUser">
      <thead>
        <tr>
          <th>Ad</th>
          <th>Soyadi</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="user in users" :key="user.id">
          <td v-text="user.firstName"></td>
          <td v-text="user.lastName"></td>
          <b-button class="DeleteUser" v-on:click="DeleteUser(user.id)">
            Sil
          </b-button>
          <b-button class="UpdateUser" @click="SelectUpdate(user)">
            Güncelle
          </b-button>
          <b-button class="ShowUser" @click="SelectUser(user)"
            >Görüntüle
          </b-button>
        </tr>
        <b-tr v-if="users.length == 0">
          <b-td colspan="4">
            <p style="text-align: center">Tanımlı Kullanıcı Bulunamadı!</p>
          </b-td>
        </b-tr>
      </tbody>
    </table>
    <div>
      <b-modal
        id="updateuser"
        class="userUpdatePopup"
        ref="my-modal"
        hide-footer
        title="Kullanici Güncelleme"
      >
        <div class="d-block text-center">
          <update-component :user="selectedUpdate" />
        </div>
      </b-modal>
    </div>
    <div>
      <b-modal
        class="showUserPopup"
        ref="showPopup"
        hide-footer
        title="Kullanici Detaylari"
      >
        <div class="showUserPopup">
          <show-component :user="selectedUser" />
        </div>
      </b-modal>
    </div>
  </div>
</template>

<script>
const baseUrl = "https://localhost:5001";
import axios from "axios";
import ShowComponent from "./ShowComponent.vue";
import UpdateComponent from "./UpdateComponent.vue";
import Swal from "sweetalert2/dist/sweetalert2.js";

export default {
  components: { ShowComponent, UpdateComponent },
  name: "GetComponent",
  data() {
    return {
      selectedUser: {},
      selectedUpdate: {},
      users: [],
      addresses: [],
      showUser: false,
      showUpdate: false,
    };
  },
  methods: {
    SelectUpdate(user) {
      this.selectedUpdate = user;
      console.log(this.selectedUpdate);
      this.showUpdate = true;
      this.$refs["my-modal"].show();
    },
    SelectUser(user) {
      this.selectedUser = user;
      console.log(this.selectedUser);
      this.showUser = true;
      this.$refs["showPopup"].show();
    },
    DeleteUser(id) {
      Swal.fire({
        title: "Silmek istediğine eminmisin?",
        text: "Bu işlem kullaniciyi kalici olarak siler",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Sil",
        cancelButtonText: "İptal Et",
      }).then((result) => {
        if (result.isConfirmed) {
          this.axios
            .delete(baseUrl + "/User/DeleteUserById?Id=" + id)
            .then((result) => {
              this.GetUser();
              console.log(result);
            });
          Swal.fire("Kullanici silindi", "İşlem Başarılı");
        }
      });
    },
    GetUser() {
      axios
        .get(baseUrl + "/User/AllUser")
        .then((response) => {
          this.users = response.data;
          console.log(response);
        })
        .catch((err) => {
          console.log(err);
        });
    },
    GetAddress() {
      axios
        .get(baseUrl + "/Address/AllAddress")
        .then((response) => {
          this.addresses = response.data;
          console.log(response);
        })
        .catch((err) => {
          console.log(err);
        });
    },
    showModal() {
      this.$bvModal.show("updateuser");
    },
    showPopupModal() {
      this.$refs["showPopup"].showPopupModal();
    },
  },
  mounted() {
    this.GetUser();
    this.GetAddress();
  },
  watch: {
    show(newVal) {
      console.log("Alert is now " + (newVal ? "visible" : "hidden"));
    },
    showPopupModal(showpopupVal) {
      console.log("Alert is now " + (showpopupVal ? "visible" : "hidden"));
    },
  },
};
</script>

<style>
h3.UserList {
  margin-left: 670px;
}
table.GetUser {
  margin-left: 360px;
}
button.DeleteUser {
  margin-left: 400px;
}
</style>
