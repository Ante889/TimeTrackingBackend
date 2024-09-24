import React from 'react';
import { Layout, theme } from 'antd';
import Breadcrumbs from '../components/Breadcrumbs';

const { Content } = Layout;

const Dashboard = () => {
  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();

  const breadcrumbItems = [
    { label: 'Dashboard', route: '/dashboard' }
  ];
console.log(localStorage.getItem('userId'))
  return (
    <Content style={{ margin: '0 16px' }}>
      <Breadcrumbs items={breadcrumbItems} />
      <div
        style={{
          padding: 24,
          minHeight: 360,
          background: colorBgContainer,
          borderRadius: borderRadiusLG,
        }}
      >
        Ovdje će ići grafovi
      </div>
    </Content>
  );
};

export default Dashboard;
