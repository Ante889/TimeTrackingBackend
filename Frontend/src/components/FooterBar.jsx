import React from 'react';
import { Layout } from 'antd';

const { Footer } = Layout;

const FooterBar = () => {
  return (
    <Footer style={{ textAlign: 'center' }}>
       Završni rad edunova ©{new Date().getFullYear()} Created by Ante Filipović
    </Footer>
  );
};

export default FooterBar;
