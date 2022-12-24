<template>
  <div class="PostComponent">
    <form class="kayitForm" @submit="postUserData" method="post">
      <input
        class="postFirstName"
        type="text"
        name="firstName"
        placeholder="Adınızı giriniz"
        required
        v-model="postsUser.firstName"
      />
      <label class="labelFirstname" for="FirstName">Adi</label><br />
      <input
        class="postLastName"
        type="text"
        name="lastName"
        placeholder="Soyadınızı giriniz"
        required
        v-model="postsUser.lastName"
      />
      <label class="labelLastName" for="LastName">Soyadi</label><br />

      <br />
      <br />
      <input
        class="postName"
        type="text"
        name="Name"
        placeholder="Adres adını giriniz"
        v-model="postsAddress.name"
      />
      <label class="labelName" for="Name">Adres Adi</label><br />
      <br />
      <br />
      <input
        class="postOpenAddress"
        type="text"
        name="OpenAddress"
        placeholder="Açık adresinizi giriniz"
        v-model="postsAddress.openAddress"
      />
      <label class="labelOpenAddress" for="OpenAddress">Açik adres</label><br />
      <br />
      <br />
      <b-button class="AddressAdd" @click="AddAddress" type="button">
        Adres Ekle
      </b-button>
      <ul>
        <table class="AddAddressTable">
          <thead>
            <tr>
              <th>Adres Adi</th>
              <th>Açik Adres</th>
            </tr>
            <tr v-for="address in postsUser.addresses" :key="address.name">
              <td v-text="address.name"></td>
              <td v-text="address.openAddress"></td>
              <b-button class="AddressDelete" @click="removeAddress(address)">
                Sil
              </b-button>
            </tr>
          </thead>
        </table>
      </ul>
      <b-button class="UserAdd" type="submit">Kullanici Kaydet</b-button>
    </form>
  </div>
</template>

<script>
import Vue from "vue";
import axios from "axios";
import VueAxios from "vue-axios";
import Swal from "sweetalert2/dist/sweetalert2.js";

Vue.use(VueAxios, axios);

const baseUrl = "https://localhost:5001";

export default {
  name: "PostComponent",
  data() {
    return {
      postsUser: {
        firstName: null,
        lastName: null,
        addresses: [],
      },
      postsAddress: {
        name: null,
        openAddress: null,
      },
    };
  },

  methods: {
    removeAddress: function (index) {
      Swal.fire("Adres silindi");
      console.log("Removing", index);
      this.postsUser.addresses.splice(index, 1);
    },
    postUserData(e) {
      e.preventDefault();
      if (this.postsUser.addresses.length == 0) {
        this.$swal({
          icon: "error",
          title: "Ooops..",
          text: "Kullanıcı kaydedebilmek için en az bir tane adres tanımı yapmanız gerekmektedir!",
        });
        return;
      }
      Swal.fire("Kullanici eklendi");
      console.log(this.postsAddress);
      axios
        .post("https://localhost:5001/User/AddUser", this.postsUser)
        .then((result) => {
          window.location.reload();
          console.warn(result);
        });
      this.$bvModal.hide("userKayit");
      e.preventDefault();

      return;
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
    postAddressData(x) {
      axios
        .post("https://localhost:5001/Address/AddAddress", this.postsAddress)
        .then((results) => {
          console.warn(results);
        });
      x.preventDefault();
    },
    AddAddress(event) {
      event.preventDefault();
      const exists = this.postsUser.addresses.filter(
        (x) => x.name === this.postsAddress.name
      );
      if (exists && exists.length > 0) {
        this.$swal({
          icon: "error",
          title: "Ooops..",
          text: "Eklemeye çalıştığınız adres ismi mevcut! Lütfen farklı isim deneyiniz!",
        });
        return;
      }
      Swal.fire("Adres eklendi");
      this.postsUser.addresses.push({
        name: this.postsAddress.name,
        openAddress: this.postsAddress.openAddress,
      });
      this.postsAddress.name = null;
      this.postsAddress.openAddress = null;
    },
  },
  mounted() {
    this.GetUser();
    console.log(this.postsUser);
    console.log(this.postsAddress);
  },
};
</script>

<style>
label.labelFirstname {
  margin-top: 0px;
  margin-right: 390px;
}
input.postFirstName {
  position: absolute;
  margin-top: 30px;
  margin-left: 0px;
}
label.labelLastName {
  margin-top: 40px;
  margin-right: 370px;
}
input.postLastName {
  position: absolute;
  margin-top: 70px;
  margin-left: 0px;
}
label.labelName {
  position: absolute;
  margin-top: -135px;
  margin-left: 20px;
}
input.postName {
  position: absolute;
  margin-top: -106px;
  margin-left: 20px;
}
label.labelOpenAddress {
  position: absolute;
  margin-top: -143px;
  margin-left: 20px;
}
input.postOpenAddress {
  position: absolute;
  margin-top: -114px;
  margin-left: 20px;
}
button.AddressDelete {
  margin-right: 80px;
}
button.UserAdd {
  margin-left: 300px;
}
button.AddressAdd {
  margin-top: -230px;
  margin-left: 320px;
}
table.AddAddressTable {
  position: absolute;
  margin-top: -150px;
  margin-left: 20px;
}
</style>
