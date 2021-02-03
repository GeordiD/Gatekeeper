<template>
  <button type="button" class="btn btn-primary" @click="onLoginClick">Login</button>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component';
import { EnvConfig } from '@/models/EnvConfig';
import { EnvConfigService } from '@/services/EnvConfigService';
import { _authStore } from '@/store/AuthStore';

export default class LoginView extends Vue {

  config: EnvConfig = {};

  async beforeMount() {
    this.config = await new EnvConfigService().getConfig();

  }

  onLoginClick() {
    window.location.href = `https://auth.tdameritrade.com/auth?response_type=code&redirect_uri=http%3A%2F%2Flocalhost%3A8080&client_id=${this.config.tdClientKey}%40AMER.OAUTHAP`;
  }
}
</script>
