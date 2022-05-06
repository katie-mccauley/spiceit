<template>
  <div class="container-fluid">
    <div class="row mt-3">
      <div class="col-1">
        <img class="rounded" :src="account.picture" alt="" />
      </div>
      <div class="col-9 d-flex align-items-center">
        <h2>{{ account.email }}</h2>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
export default {
  name: 'Account',
  setup() {
    return {
      account: computed(() => AppState.account)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
