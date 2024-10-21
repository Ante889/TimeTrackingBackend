import React from 'react';
import { Layout, Menu } from 'antd';
import { Link, useLocation } from 'react-router-dom';
import Cookies from 'universal-cookie';
import {
  DesktopOutlined,
  AppstoreAddOutlined,
  PictureOutlined,
  UserAddOutlined,
  UserOutlined
} from '@ant-design/icons';
import { getCookieToken } from '../service/cookieService';

const { Sider } = Layout;

function getItem(label, key, icon, route, items) {
  if (route && route.startsWith('http')) {
    return {
      key,
      icon,
      items,
      label: <a href={route} target="_blank" rel="noopener noreferrer">{label}</a>,
    };
  }
  return {
    key,
    icon,
    items,
    label: route ? <Link to={route}>{label}</Link> : label,
  };
}

const protectedItems = [
  getItem('Login', '4', <UserOutlined />, '/login'),
  getItem('Register', '5', <UserAddOutlined />, '/register')
];

const publicItems = [
  getItem('Sql graph', '1', <PictureOutlined />, '/base-image'),
  getItem('Projects', '2', <AppstoreAddOutlined />, '/projects'),
  getItem('Swagger', '3', <DesktopOutlined />, 'https://antefilipovic-001-site1.ftempurl.com/swagger/index.html')
];

const Sidebar = ({ collapsed, setCollapsed }) => {
  const location = useLocation();
  const currentPath = location.pathname;
  const cookies = new Cookies();
  const token = getCookieToken();
  const menuItems = token ? publicItems : protectedItems;

  return (
    <Sider collapsible collapsed={collapsed} onCollapse={setCollapsed}>
      <div className="demo-logo-vertical" />
      <Menu
        theme="dark"
        mode="inline"
        selectedKeys={[menuItems.find(item => item.route === currentPath)?.key || '']}
      >
        {menuItems.map(item => (
          <Menu.Item key={item.key} icon={item.icon}>
            {item.label}
          </Menu.Item>
        ))}
      </Menu>
    </Sider>
  );
};

export default Sidebar;
