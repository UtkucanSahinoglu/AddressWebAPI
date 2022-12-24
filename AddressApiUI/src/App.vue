<template>
  <div class="AppClass" id="app">
    <h3 class="h3User">
      Kullanici ve Adres Düzenleme <b-badge variant="light">Light</b-badge>
    </h3>
    <div>
      <b-button class="OpenUserAddButton" id="show-btn" @click="showModal"
        >Kullanici Ekle
      </b-button>
      <b-modal
        id="userKayit"
        class="userKayitPopup"
        ref="my-modal"
        hide-footer
        title="User Kayit"
      >
        <div class="d-block text-center">
          <post-component />
        </div>
      </b-modal>
    </div>
    <get-component />
  </div>
</template>

<script>
import PostComponent from "./components/PostComponent";
import GetComponent from "./components/GetComponent";
export default {
  name: "App",
  data() {
    return {};
  },
  components: {
    PostComponent,
    GetComponent,
  },
  methods: {
    showModal() {
      this.$bvModal.show("userKayit");
    },
  },
};
</script>

<style>
h3.h3User {
  margin-left: 600px;
}
button.OpenUserAddButton {
  margin-left: 1200px;
}
</style>
