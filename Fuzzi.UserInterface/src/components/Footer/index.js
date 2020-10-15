import React from 'react';
import { FooterBase, Image } from './styles';

function Footer() {
  return (
    <FooterBase>
      <a href="https://bxblue.com.br/">
        <Image src="https://bxblue.com.br/aprenda/wp-content/uploads/2018/01/bxblue-newlogo.png" alt="Logo bxblue" />
      </a>
      <p>
        Made by
        {' '}
        <a href="https://github.com/guilemoss/bxblue">guilemoss</a>
      </p>
    </FooterBase>
  );
}

export default Footer;